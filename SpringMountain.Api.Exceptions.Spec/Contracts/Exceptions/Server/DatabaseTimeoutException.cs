using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 数据库连接超时异常（500）
/// </summary>
public class DatabaseTimeoutException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.DatabaseTimeout;

    public override string Status { get; } = "DATABASE_TIMEOUT";

    public override string Message { get; } = "数据库连接超时";

    public DatabaseTimeoutException()
    {
    }

    public DatabaseTimeoutException(string message) : base(message)
    {
        this.Message = message;
    }

    public DatabaseTimeoutException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public DatabaseTimeoutException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public DatabaseTimeoutException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}