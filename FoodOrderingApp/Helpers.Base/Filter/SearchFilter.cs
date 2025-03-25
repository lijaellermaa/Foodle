namespace Helpers.Base.Filter;

public class SearchFilter<TValue>(Func<TValue, bool> searchFunc)
    : IFilter<TValue>
{
    private Func<TValue, bool> SearchFunc { get; } = searchFunc;

    public IEnumerable<TValue> Apply(IEnumerable<TValue> entity)
    {
        return entity
            .Where(SearchFunc);
    }
}