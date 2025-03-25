namespace Helpers.Base.Filter;

public class PageFilter<TValue>(int take = 10, int skip = 0)
    : IFilter<TValue>
{
    public IEnumerable<TValue> Apply(IEnumerable<TValue> entity)
    {
        return entity
            .Skip(skip)
            .Take(take);
    }
}