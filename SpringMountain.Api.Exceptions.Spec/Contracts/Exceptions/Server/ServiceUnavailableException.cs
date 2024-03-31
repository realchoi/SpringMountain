using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 服务不可用异常（503）
/// </summary>
public class ServiceUnavailableException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.ServiceUnavailable;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.ServiceUnavailable;

    public override string Status { get; } = "SERVICE_UNAVAILABLE";

    public override string Message { get; } = "服务不可用";

    public ServiceUnavailableException()
    {
    }

    public ServiceUnavailableException(string message) : base(message)
    {
        this.Message = message;
    }

    public ServiceUnavailableException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public ServiceUnavailableException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public ServiceUnavailableException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}