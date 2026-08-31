using BookOrdering.Domain.Ordering;
using BookOrdering.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace BookOrdering.Tests.Configuration;

public sealed class ConfigurationSortCriteriaProviderTests
{
    [Fact]
    public void GetSortCriteria_ReadsAttributesAndDirectionsFromConfiguration_InDeclaredOrder()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["BookOrdering:SortCriteria:0:Attribute"] = "EditionYear",
                ["BookOrdering:SortCriteria:0:Direction"] = "Descending",
                ["BookOrdering:SortCriteria:1:Attribute"] = "Title",
                ["BookOrdering:SortCriteria:1:Direction"] = "Ascending"
            })
            .Build();

        var sortCriteriaProvider = new ConfigurationSortCriteriaProvider(configuration);

        var sortCriteria = sortCriteriaProvider.GetSortCriteria();

        Assert.Equal(
        [
            new SortCriterion(BookSortAttribute.EditionYear, SortDirection.Descending),
            new SortCriterion(BookSortAttribute.Title, SortDirection.Ascending)
        ], sortCriteria);
    }

    [Fact]
    public void GetSortCriteria_MissingSection_ThrowsInvalidOperationException()
    {
        var configuration = new ConfigurationBuilder().Build();
        var sortCriteriaProvider = new ConfigurationSortCriteriaProvider(configuration);

        Assert.Throws<InvalidOperationException>(() => sortCriteriaProvider.GetSortCriteria());
    }
}
