using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 权限认证失败异常（403）
/// </summary>
public class ForbiddenException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.Forbidden;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.Forbidden;

    public override string Status { get; } = "FORBIDDEN";

    public override string Message { get; } = "权限认证失败";

    public ForbiddenException()
    {
    }

    public ForbiddenException(string message) : base(message)
    {
        this.Message = message;
    }

    public ForbiddenException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public ForbiddenException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public ForbiddenException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}