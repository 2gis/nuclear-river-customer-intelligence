using System.Collections.Generic;
using System.Linq;

using NuClear.River.Common.Metadata.Elements;
using NuClear.River.Common.Metadata.Model.Operations;
using NuClear.Storage.API.Specifications;

namespace NuClear.Replication.Core.Aggregates
{
    public sealed class ValueObjectFindSpecificationProvider<T, TKey> : IFindSpecificationProvider<T>
    {
        private readonly ValueObjectMetadata<T, TKey> _metadata;

        public ValueObjectFindSpecificationProvider(ValueObjectMetadata<T, TKey> metadata)
        {
            _metadata = metadata;
        }

        public FindSpecification<T> Create(IEnumerable<AggregateOperation> commands)
        {
            // todo: ��� ���� �� ���� ����������� �� �������� �������� TKey... (��. ������ "���������� ���������")
            return _metadata.FindSpecificationProvider.Invoke(commands.Select(c => c.AggregateId).Cast<TKey>().ToArray());
        }
    }
}