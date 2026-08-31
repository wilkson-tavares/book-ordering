namespace BookOrdering.Domain.Ordering;

/// <summary>
/// Pairs a <see cref="BookSortAttribute"/> with the <see cref="SortDirection"/> it should be ordered by.
/// A full ordering is expressed as an ordered sequence of criteria, applied from the most to the least significant.
/// </summary>
/// <param name="Attribute">The book attribute to sort by.</param>
/// <param name="Direction">The direction to sort <paramref name="Attribute"/> by.</param>
public sealed record SortCriterion(BookSortAttribute Attribute, SortDirection Direction);
