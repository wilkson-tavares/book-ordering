using BookOrdering.Application.Ordering;
using BookOrdering.Domain.Ordering;

namespace BookOrdering.Tests.TestDoubles;

/// <summary>
/// Test double that returns a fixed, caller-supplied sequence of <see cref="SortCriterion"/>,
/// so ordering logic can be tested without a real configuration source.
/// </summary>
internal sealed class StubSortCriteriaProvider : ISortCriteriaProvider
{
    private readonly IReadOnlyList<SortCriterion> _sortCriteria;

    public StubSortCriteriaProvider(params SortCriterion[] sortCriteria)
    {
        _sortCriteria = sortCriteria;
    }

    public IReadOnlyList<SortCriterion> GetSortCriteria() => _sortCriteria;
}
