using BookOrdering.Domain;
using BookOrdering.Domain.Exceptions;
using BookOrdering.Domain.Ordering;

namespace BookOrdering.Application.Ordering;

/// <summary>
/// Use case implementation of <see cref="IBookOrderingService"/>. Sorts books by the criteria returned from
/// an injected <see cref="ISortCriteriaProvider"/>, applying each criterion in turn as a tie-breaker
/// for the ones before it.
/// </summary>
public sealed class BookOrderingService : IBookOrderingService
{
    private readonly ISortCriteriaProvider _sortCriteriaProvider;

    /// <summary>
    /// Creates a <see cref="BookOrderingService"/> that sorts using the criteria from <paramref name="sortCriteriaProvider"/>.
    /// </summary>
    public BookOrderingService(ISortCriteriaProvider sortCriteriaProvider)
    {
        _sortCriteriaProvider = sortCriteriaProvider ?? throw new ArgumentNullException(nameof(sortCriteriaProvider));
    }

    /// <inheritdoc />
    public IReadOnlyList<Book> Order(IReadOnlyCollection<Book>? books)
    {
        if (books is null)
        {
            throw new BookOrderingException("The collection of books to order must not be null.");
        }

        var sortCriteria = _sortCriteriaProvider.GetSortCriteria();
        var orderedBooks = books.ToList();
        orderedBooks.Sort(BuildComparison(sortCriteria));
        return orderedBooks;
    }

    /// <summary>
    /// Combines <paramref name="sortCriteria"/>, in order, into a single <see cref="Comparison{T}"/> where
    /// each criterion breaks ties left unresolved by the criteria before it.
    /// </summary>
    private static Comparison<Book> BuildComparison(IReadOnlyList<SortCriterion> sortCriteria) =>
        (left, right) =>
        {
            foreach (var criterion in sortCriteria)
            {
                var comparisonResult = CompareByAttribute(left, right, criterion.Attribute);
                if (criterion.Direction == SortDirection.Descending)
                {
                    comparisonResult = -comparisonResult;
                }

                if (comparisonResult != 0)
                {
                    return comparisonResult;
                }
            }

            return 0;
        };

    /// <summary>
    /// Compares two books by a single <see cref="BookSortAttribute"/>, using ordinal comparison for text
    /// attributes so results are independent of the running machine's culture.
    /// </summary>
    private static int CompareByAttribute(Book left, Book right, BookSortAttribute attribute) => attribute switch
    {
        BookSortAttribute.Title => string.CompareOrdinal(left.Title, right.Title),
        BookSortAttribute.AuthorName => string.CompareOrdinal(left.AuthorName, right.AuthorName),
        BookSortAttribute.EditionYear => left.EditionYear.CompareTo(right.EditionYear),
        _ => throw new ArgumentOutOfRangeException(nameof(attribute), attribute, "Unsupported book sort attribute.")
    };
}
