using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 外部服务不可用异常
/// </summary>
public class ExternalUnavailableException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.ExternalUnavailable;

    public override string Status { get; } = "EXTERNAL_UNAVAILABLE";

    public override string Message { get; } = "外部服务不可用";

    public ExternalUnavailableException()
    {
    }

    public ExternalUnavailableException(string message) : base(message)
    {
        this.Message = message;
    }

    public ExternalUnavailableException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public ExternalUnavailableException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public ExternalUnavailableException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}