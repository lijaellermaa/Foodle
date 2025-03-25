using System.Collections;
using Helpers.Base.Entities;

namespace Helpers.Base.Filter;

public class SortFilter<TValue, TKey>(Func<TValue, TKey> keySelector, SortDirection sortDirection = SortDirection.None) 
    : IFilter<TValue>
{
    private SortDirection SortDirection { get; } = sortDirection;
    private Func<TValue, TKey> KeySelector { get; } = keySelector;

    public IEnumerable<TValue> Apply(IEnumerable<TValue> entity)
    {
        return SortDirection switch
        {
            SortDirection.Ascending => entity.OrderBy(KeySelector),
            SortDirection.Descending => entity.OrderByDescending(KeySelector),
            _ => entity
        };
    }
}