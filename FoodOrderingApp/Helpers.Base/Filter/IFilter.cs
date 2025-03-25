using System.Collections;

namespace Helpers.Base.Filter;

public interface IFilter<TValue>
{
    public IEnumerable<TValue> Apply(IEnumerable<TValue> entity);
}