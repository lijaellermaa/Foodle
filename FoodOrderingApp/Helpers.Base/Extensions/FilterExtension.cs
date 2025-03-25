using Helpers.Base.Filter;

namespace Helpers.Base.Extensions;

public static class FilterExtension
{
    public static IEnumerable<TValue> Filter<TValue>(this IEnumerable<TValue> entity, IFilter<TValue> filter)
    {
        return filter.Apply(entity);
    }

    public static async Task<IEnumerable<TValue>> FilterAsync<TValue>(this Task<IEnumerable<TValue>> entity, IFilter<TValue> filter)
    {
        var result = await entity;
        return filter.Apply(result);
    }
}