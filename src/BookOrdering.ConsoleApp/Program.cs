using BookOrdering.Application.Ordering;
using BookOrdering.Domain;
using BookOrdering.Domain.Exceptions;
using BookOrdering.Domain.Ordering;
using BookOrdering.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

// Composition root: wires the sort criteria configured in appsettings.json to the ordering use case.
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

ISortCriteriaProvider sortCriteriaProvider = new ConfigurationSortCriteriaProvider(configuration);
IBookOrderingService bookOrderingService = new BookOrderingService(sortCriteriaProvider);

var books = new List<Book>
{
    new("Java How to Program", "Deitel & Deitel", 2007),
    new("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002),
    new("Head First Design Patterns", "Elisabeth Freeman", 2004),
    new("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007)
};

Console.WriteLine("Sort criteria (from appsettings.json):");
foreach (var criterion in sortCriteriaProvider.GetSortCriteria())
{
    Console.WriteLine($"  {criterion.Attribute} {criterion.Direction}");
}

Console.WriteLine();
Console.WriteLine("Ordered books:");
foreach (var book in bookOrderingService.Order(books))
{
    Console.WriteLine($"  {book.Title} - {book.AuthorName} ({book.EditionYear})");
}

try
{
    bookOrderingService.Order(null);
}
catch (BookOrderingException exception)
{
    Console.WriteLine();
    Console.WriteLine($"Ordering a null collection throws {nameof(BookOrderingException)}: {exception.Message}");
}
