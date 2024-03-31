using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 身份验证失败异常（401）
/// </summary>
public class UnauthenticatedException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.Unauthorized;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.Unauthenticated;

    public override string Status { get; } = "UNAUTHENTICATED";

    public override string Message { get; } = "身份验证失败";

    public UnauthenticatedException()
    {
    }

    public UnauthenticatedException(string message) : base(message)
    {
        this.Message = message;
    }

    public UnauthenticatedException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public UnauthenticatedException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public UnauthenticatedException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}