using System.Collections;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming.Mergers
{
    internal interface IValueObjectMerger
    {
        // TODO {m.pashuk, 15.07.2015}: ���������� value objects ������� � ��������� ������, ������ ���� ����� �������
        MergeTool.IMergeResult Merge(IEnumerable source, IEnumerable target);
    }
}