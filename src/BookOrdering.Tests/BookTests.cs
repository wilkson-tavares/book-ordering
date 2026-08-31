using BookOrdering.Domain;

namespace BookOrdering.Tests;

public sealed class BookTests
{
    [Fact]
    public void Equals_SameTitleAuthorAndEditionYear_AreEqual()
    {
        var first = new Book("Java How to Program", "Deitel & Deitel", 2007);
        var second = new Book("Java How to Program", "Deitel & Deitel", 2007);

        Assert.Equal(first, second);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_BlankTitle_ThrowsArgumentException(string? title)
    {
        Assert.Throws<ArgumentException>(() => new Book(title!, "Some Author", 2020));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_BlankAuthorName_ThrowsArgumentException(string? authorName)
    {
        Assert.Throws<ArgumentException>(() => new Book("Some Title", authorName!, 2020));
    }

    [Fact]
    public void Constructor_NonPositiveEditionYear_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Book("Some Title", "Some Author", 0));
    }
}
