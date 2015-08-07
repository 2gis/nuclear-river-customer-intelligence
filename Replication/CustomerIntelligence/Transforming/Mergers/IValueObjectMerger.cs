using System.Collections;

using NuClear.AdvancedSearch.Replication.API.Transforming;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming.Mergers
{
    internal interface IValueObjectMerger
    {
        // TODO {m.pashuk, 15.07.2015}: ���������� value objects ������� � ��������� ������, ������ ���� ����� �������
        IMergeResult Merge(IEnumerable source, IEnumerable target);
    }
}