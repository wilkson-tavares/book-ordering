using BookOrdering.Domain.Ordering;

namespace BookOrdering.Application.Ordering;

/// <summary>
/// Supplies the ordered sequence of <see cref="SortCriterion"/> that <see cref="BookOrderingService"/> applies.
/// Implementations decide where the criteria come from (e.g. a configuration file), keeping that choice
/// out of the use case and out of the domain contract.
/// </summary>
public interface ISortCriteriaProvider
{
    /// <summary>
    /// Gets the sort criteria to apply, from the most to the least significant.
    /// </summary>
    IReadOnlyList<SortCriterion> GetSortCriteria();
}
