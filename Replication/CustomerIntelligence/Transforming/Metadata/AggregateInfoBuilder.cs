using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using NuClear.AdvancedSearch.Replication.CustomerIntelligence.Data.Context;
using NuClear.AdvancedSearch.Replication.Model;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming.Metadata
{
    internal static class AggregateInfoBuilder
    {
        public static AggregateInfoBuilder<TAggregate> OfType<TAggregate>()
            where TAggregate : ICustomerIntelligenceObject, IIdentifiable
        {
            return new AggregateInfoBuilder<TAggregate>();
        }
    }

    internal class AggregateInfoBuilder<TAggregate>
        where TAggregate : ICustomerIntelligenceObject, IIdentifiable
    {
        private Func<ICustomerIntelligenceContext, IEnumerable<long>, IQueryable<TAggregate>> _queryByIds;
        private readonly List<IEntityInfo> _entities;
        private readonly List<IValueObjectInfo> _valueObjects;

        public AggregateInfoBuilder()
        {
            _entities = new List<IEntityInfo>();
            _valueObjects = new List<IValueObjectInfo>();
        }

        public IAggregateInfo Build()
        {
            return new AggregateInfo<TAggregate>(_queryByIds, _entities, _valueObjects);
        }

        public AggregateInfoBuilder<TAggregate> HasSource(Func<ICustomerIntelligenceContext, IQueryable<TAggregate>> queryProvider)
        {
            _queryByIds = CreateFilteredQueryProvider(queryProvider, CreateKeyAccessor<TAggregate>());
            return this;
        }

        public AggregateInfoBuilder<TAggregate> HasValueObject<TValueObject>(Func<ICustomerIntelligenceContext, IQueryable<TValueObject>> queryProvider, Expression<Func<TValueObject, long>> parentIdSelector)
        {
            var queryByParentIds = CreateFilteredQueryProvider(queryProvider, parentIdSelector);
            _valueObjects.Add(new ValueObjectInfo<TValueObject>(queryByParentIds));
            return this;
        }

        public AggregateInfoBuilder<TAggregate> HasEntity<TEntity>(Func<ICustomerIntelligenceContext, IQueryable<TEntity>> queryProvider, Expression<Func<TEntity, long>> parentIdSelector)
            where TEntity : IIdentifiable
        {
            var queryByIds = CreateFilteredQueryProvider(queryProvider, CreateKeyAccessor<TEntity>());
            var queryByParentIds = CreateFilteredQueryProvider(queryProvider, parentIdSelector);
            _entities.Add(new EntityInfo<TEntity>(queryByIds, queryByParentIds));
            return this;
        }

        private static Func<ICustomerIntelligenceContext, IEnumerable<long>, IQueryable<T>> CreateFilteredQueryProvider<T>(Func<ICustomerIntelligenceContext, IQueryable<T>> queryProvider, Expression<Func<T, long>> idSelector)
        {
            return (context, ids) =>
            {
                var query = queryProvider(context);
                var filterExpression = CreateFilterExpression(ids, idSelector);
                return query.Where(filterExpression);
            };
        }

        private static Expression<Func<T, long>> CreateKeyAccessor<T>()
        {
            // ���� �������� (TAggregate x) => x.Id, �� � ���� ��������� �������� Id ����� �������� �� � ���� TAggregate, � � ���� IIdentifiable
            // � ����������� ������� ��� ��� �����, �� ��� ����� ����� ���� � linq2db - ��� ���������� ������, ��� ��������� ������ �������� 
            // ������ ���� TAggregate � �� ������ �� �����, ��� ����� IIdentifiable.Id
            var param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, long>>(Expression.Property(param, "Id"), param);
        }

        private static Expression<Func<T, bool>> CreateFilterExpression<T>(IEnumerable<long> ids, Expression<Func<T, long>> idSelector)
        {
            Expression<Func<T, bool>> example = foo => ids.Contains(0);
            var exampleMethodCall = (MethodCallExpression)example.Body;
            var methodCall = exampleMethodCall.Update(null, new[] { exampleMethodCall.Arguments[0], idSelector.Body });
            return Expression.Lambda<Func<T, bool>>(methodCall, idSelector.Parameters);
        }
    }
}