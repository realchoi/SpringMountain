using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 数据库不可用异常
/// </summary>
public class DatabaseUnavailableException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.DatabaseUnavailable;

    public override string Status { get; } = "DATABASE_UNAVAILABLE";

    public override string Message { get; } = "数据库不可用";

    public DatabaseUnavailableException()
    {
    }

    public DatabaseUnavailableException(string message) : base(message)
    {
        this.Message = message;
    }

    public DatabaseUnavailableException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public DatabaseUnavailableException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public DatabaseUnavailableException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}