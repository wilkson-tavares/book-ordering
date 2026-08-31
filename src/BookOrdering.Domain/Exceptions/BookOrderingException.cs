namespace BookOrdering.Domain.Exceptions;

/// <summary>
/// Thrown when a <see cref="Ordering.IBookOrderingService"/> cannot order a requested collection of books.
/// </summary>
public sealed class BookOrderingException : Exception
{
    /// <summary>Creates a <see cref="BookOrderingException"/> with the given error message.</summary>
    public BookOrderingException(string message)
        : base(message)
    {
    }

    /// <summary>Creates a <see cref="BookOrderingException"/> with the given error message and inner exception.</summary>
    public BookOrderingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
