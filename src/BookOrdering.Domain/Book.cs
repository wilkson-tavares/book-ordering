namespace BookOrdering.Domain;

/// <summary>
/// Represents a book as an immutable domain value object, identified by the
/// combination of its <see cref="Title"/>, <see cref="AuthorName"/> and <see cref="EditionYear"/>.
/// </summary>
/// <param name="Title">The title of the book.</param>
/// <param name="AuthorName">The name of the book's author.</param>
/// <param name="EditionYear">The year the book edition was published.</param>
public sealed record Book(string Title, string AuthorName, int EditionYear)
{
    public string Title { get; } = string.IsNullOrWhiteSpace(Title)
        ? throw new ArgumentException("The book title must not be null or empty.", nameof(Title))
        : Title;

    public string AuthorName { get; } = string.IsNullOrWhiteSpace(AuthorName)
        ? throw new ArgumentException("The book author name must not be null or empty.", nameof(AuthorName))
        : AuthorName;

    public int EditionYear { get; } = EditionYear > 0
        ? EditionYear
        : throw new ArgumentOutOfRangeException(nameof(EditionYear), EditionYear, "The book edition year must be greater than zero.");
}
