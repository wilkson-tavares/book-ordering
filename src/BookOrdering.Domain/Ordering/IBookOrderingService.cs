namespace BookOrdering.Domain.Ordering;

/// <summary>
/// Domain service that orders a collection of <see cref="Book"/> instances by one or more attributes.
/// The attributes considered and their sort directions are an implementation concern, not part of this contract.
/// </summary>
public interface IBookOrderingService
{
    /// <summary>
    /// Returns <paramref name="books"/> ordered according to the currently configured sort criteria.
    /// </summary>
    /// <param name="books">The books to order. Must not be <see langword="null"/>.</param>
    /// <returns>A new list containing the books from <paramref name="books"/> in sorted order.</returns>
    /// <exception cref="Exceptions.BookOrderingException">Thrown when <paramref name="books"/> is <see langword="null"/>.</exception>
    IReadOnlyList<Book> Order(IReadOnlyCollection<Book>? books);
}
