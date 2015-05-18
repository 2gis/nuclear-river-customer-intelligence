using System;
using System.Collections.Generic;

using NuClear.AdvancedSearch.Replication.CustomerIntelligence.Model.Facts;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Transforming
{
    internal sealed class FactTypePriorityComparer : IComparer<Type>
    {
        // ����������� ������ �������� ������ ����� ���������
        // �������, ��� � ����� TUC ��� ������ ���� ���������� ������, ��� ����������� � ��� �������� � valueobject'�
        private static readonly Dictionary<Type, int> Priority
            = new Dictionary<Type, int>
              {
                  { typeof(Territory), 1 },
                  { typeof(Category), 1 },
                  { typeof(CategoryGroup), 1 },
                  { typeof(Project), 1 },
                  { typeof(Firm), 1 },
                  { typeof(Client), 1 },
              };

        public int Compare(Type x, Type y)
        {
            int priorityX;
            if (!Priority.TryGetValue(x, out priorityX))
            {
                priorityX = 0;
            }

            int priorityY;
            if (!Priority.TryGetValue(y, out priorityY))
            {
                priorityY = 0;
            }

            return priorityX - priorityY;
        }
    }
}