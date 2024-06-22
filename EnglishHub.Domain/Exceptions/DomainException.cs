namespace EnglishHub.Domain.Exceptions;

public class DomainException : Exception
{
    public ErrorCode ErrorCode { get; }

    public DomainException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}