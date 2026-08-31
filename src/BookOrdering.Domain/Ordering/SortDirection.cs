namespace BookOrdering.Domain.Ordering;

/// <summary>
/// Specifies the direction in which a <see cref="SortCriterion"/> orders books.
/// </summary>
public enum SortDirection
{
    /// <summary>Order from the lowest to the highest value.</summary>
    Ascending,

    /// <summary>Order from the highest to the lowest value.</summary>
    Descending
}
