using BookOrdering.Application.Ordering;
using BookOrdering.Domain;
using BookOrdering.Domain.Exceptions;
using BookOrdering.Domain.Ordering;
using BookOrdering.Tests.TestDoubles;

namespace BookOrdering.Tests.Ordering;

public sealed class BookOrderingServiceTests
{
    // Book 1-4, as numbered in the FGV technical evaluation's test case document.
    private static readonly Book Book1 = new("Java How to Program", "Deitel & Deitel", 2007);
    private static readonly Book Book2 = new("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002);
    private static readonly Book Book3 = new("Head First Design Patterns", "Elisabeth Freeman", 2004);
    private static readonly Book Book4 = new("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007);

    private static readonly IReadOnlyList<Book> AllBooks = [Book1, Book2, Book3, Book4];

    [Fact]
    public void Order_ByTitleAscendingThenAuthorNameAscending_ReturnsBooks3421()
    {
        var sortCriteriaProvider = new StubSortCriteriaProvider(
            new SortCriterion(BookSortAttribute.Title, SortDirection.Ascending),
            new SortCriterion(BookSortAttribute.AuthorName, SortDirection.Ascending));
        var bookOrderingService = new BookOrderingService(sortCriteriaProvider);

        var orderedBooks = bookOrderingService.Order(AllBooks);

        Assert.Equal([Book3, Book4, Book1, Book2], orderedBooks);
    }

    [Fact]
    public void Order_ByAuthorNameAscendingThenTitleDescending_ReturnsBooks1432()
    {
        var sortCriteriaProvider = new StubSortCriteriaProvider(
            new SortCriterion(BookSortAttribute.AuthorName, SortDirection.Ascending),
            new SortCriterion(BookSortAttribute.Title, SortDirection.Descending));
        var bookOrderingService = new BookOrderingService(sortCriteriaProvider);

        var orderedBooks = bookOrderingService.Order(AllBooks);

        Assert.Equal([Book1, Book4, Book3, Book2], orderedBooks);
    }

    [Fact]
    public void Order_ByEditionYearDescendingThenAuthorNameDescendingThenTitleAscending_ReturnsBooks4132()
    {
        var sortCriteriaProvider = new StubSortCriteriaProvider(
            new SortCriterion(BookSortAttribute.EditionYear, SortDirection.Descending),
            new SortCriterion(BookSortAttribute.AuthorName, SortDirection.Descending),
            new SortCriterion(BookSortAttribute.Title, SortDirection.Ascending));
        var bookOrderingService = new BookOrderingService(sortCriteriaProvider);

        var orderedBooks = bookOrderingService.Order(AllBooks);

        Assert.Equal([Book4, Book1, Book3, Book2], orderedBooks);
    }

    [Fact]
    public void Order_NullBooks_ThrowsBookOrderingException()
    {
        var bookOrderingService = new BookOrderingService(new StubSortCriteriaProvider(
            new SortCriterion(BookSortAttribute.Title, SortDirection.Ascending)));

        Assert.Throws<BookOrderingException>(() => bookOrderingService.Order(null));
    }

    [Fact]
    public void Order_EmptyBooks_ReturnsEmptyList()
    {
        var bookOrderingService = new BookOrderingService(new StubSortCriteriaProvider(
            new SortCriterion(BookSortAttribute.Title, SortDirection.Ascending)));

        var orderedBooks = bookOrderingService.Order([]);

        Assert.Empty(orderedBooks);
    }
}
