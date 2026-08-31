namespace BookOrdering.Domain.Ordering;

/// <summary>
/// Identifies a <see cref="Book"/> property that the ordering service can sort by.
/// </summary>
public enum BookSortAttribute
{
    /// <summary>Sort by <see cref="Book.Title"/>.</summary>
    Title,

    /// <summary>Sort by <see cref="Book.AuthorName"/>.</summary>
    AuthorName,

    /// <summary>Sort by <see cref="Book.EditionYear"/>.</summary>
    EditionYear
}
