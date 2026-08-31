using BookOrdering.Application.Ordering;
using BookOrdering.Domain.Ordering;
using Microsoft.Extensions.Configuration;

namespace BookOrdering.Infrastructure.Configuration;

/// <summary>
/// Reads the book ordering <see cref="SortCriterion"/> sequence from the "BookOrdering:SortCriteria" section
/// of an <see cref="IConfiguration"/> source, so the attributes and directions used to sort books can be
/// changed through a configuration file, without touching any code.
/// </summary>
public sealed class ConfigurationSortCriteriaProvider : ISortCriteriaProvider
{
    private const string SortCriteriaSectionKey = "BookOrdering:SortCriteria";

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Creates a <see cref="ConfigurationSortCriteriaProvider"/> that reads sort criteria from <paramref name="configuration"/>.
    /// </summary>
    public ConfigurationSortCriteriaProvider(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <inheritdoc />
    public IReadOnlyList<SortCriterion> GetSortCriteria()
    {
        var sortCriteria = _configuration.GetSection(SortCriteriaSectionKey).Get<List<SortCriterion>>();

        if (sortCriteria is null || sortCriteria.Count == 0)
        {
            throw new InvalidOperationException(
                $"Configuration section '{SortCriteriaSectionKey}' must define at least one sort criterion.");
        }

        return sortCriteria;
    }
}
