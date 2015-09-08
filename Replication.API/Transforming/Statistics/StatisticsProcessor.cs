using System.Collections.Generic;
using System.Linq;

using NuClear.Storage.Readings;

namespace NuClear.AdvancedSearch.Replication.API.Transforming.Statistics
{
    public class StatisticsProcessor<T> : IStatisticsProcessor
    {
        private readonly StatisticsInfo<T> _metadata;

        public StatisticsProcessor(StatisticsInfo<T> metadata)
        {
            _metadata = metadata;
        }

        public void RecalculateStatistics(IQuery query, IDataChangesApplier changesApplier, long projectId, IReadOnlyCollection<long?> categoryIds)
        {
            var filter = _metadata.FindSpecificationProvider.Invoke(projectId, categoryIds);

            // ������� ����������� �������� ������������� ������,
            // ����� �������� �� �� �������������, ������� ��������� �� ��������������.
            var intermediateResult = MergeTool.Merge(
                _metadata.SourceMappingSpecification.Invoke(filter).Map(query).ToList(),
                _metadata.TargetMappingSpecification.Invoke(filter).Map(query).ToList(),
                _metadata.FieldComparer);

            var changes = MergeTool.Merge(intermediateResult.Difference, intermediateResult.Complement);

            // ������� ��� ���������� ���������� - �� ����� ��������� ��� ������� ������� � ����.
            // ������� ������ ����������.
            changesApplier.Update(changes.Intersection.AsQueryable());
        }
    }
}