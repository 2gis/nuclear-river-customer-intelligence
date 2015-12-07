﻿using NuClear.AdvancedSearch.Common.Metadata.Model.Operations;

namespace NuClear.CustomerIntelligence.Replication.Tests.Transformation
{
    internal abstract class TransformationFixtureBase : DataFixtureBase
    {
        protected static class Fact
        {
            public static FactOperation Operation<T>(long entityId)
            {
                return new FactOperation(typeof(T), entityId);
            }
        }

        protected static class Aggregate
        {
            public static AggregateOperation Initialize<T>(long entityId)
            {
                return new InitializeAggregate(typeof(T), entityId);
            }

            public static AggregateOperation Recalculate<T>(long entityId)
            {
                return new RecalculateAggregate(typeof(T), entityId);
            }

            public static AggregateOperation Destroy<T>(long entityId)
            {
                return new DestroyAggregate(typeof(T), entityId);
            }
        }

        protected static class Statistics
        {
            public static RecalculateStatisticsOperation Operation(long projectId, long? categoryId = null)
            {
                return new RecalculateStatisticsOperation { ProjectId = projectId, CategoryId = categoryId };
            }
        }
    }
}