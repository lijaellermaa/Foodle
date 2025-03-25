namespace Helpers.Base.Filter;

public class AggregatedFilter<T> : IFilter<T>
{
    private ICollection<IFilter<T>> Filters { get; }

    public AggregatedFilter(IEnumerable<IFilter<T>> filters)
    {
        Filters = filters.ToList();
    }

    public AggregatedFilter()
    {
        Filters = new List<IFilter<T>>();
    }
    public AggregatedFilter(ICollection<IFilter<T>> filters)
    {
        Filters = filters;
    }

    public AggregatedFilter<T> Add(IFilter<T> filter)
    {
        Filters.Add(filter);
        return this;
    }

    public AggregatedFilter<T> AddIf(IFilter<T> filter, bool condition)
    {
        if (condition)
        {
            Filters.Add(filter);
        }
        return this;
    }

    public IEnumerable<T> Apply(IEnumerable<T> entity)
    {
        return Filters.Aggregate(entity, (current, filter) => filter.Apply(current));
    }
}