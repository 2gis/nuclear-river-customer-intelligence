using System.Collections;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming.DataMappers
{
    internal interface IValueObjectDataMapper : ITypedDataMapper
    {
        // TODO {m.pashuk, 15.07.2015}: ���������� value objects ������� � ��������� ������, ������ ���� ����� �������
        MergeTool.IMergeResult Merge(IEnumerable items1, IEnumerable items2);
    }
}