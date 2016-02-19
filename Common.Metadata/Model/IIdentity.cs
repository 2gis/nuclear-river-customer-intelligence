using System;
using System.Linq.Expressions;

namespace NuClear.AdvancedSearch.Common.Metadata.Model
{
    /// <summary>
    /// ������������ ����� ��� ������������ ����� ����� ��������� � � ���������������.
    /// </summary>
    /// <typeparam name="TKey">��� ��������������</typeparam>
    public interface IIdentity<TKey>
    {
        Expression<Func<TIdentifiable, TKey>> ExtractIdentity<TIdentifiable>();
    }
}