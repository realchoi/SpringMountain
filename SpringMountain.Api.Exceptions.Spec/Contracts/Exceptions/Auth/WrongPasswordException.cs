using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 密码错误异常（401）
/// </summary>
public class WrongPasswordException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.Unauthorized;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.WrongPassword;

    public override string Status { get; } = "WRONG_PASSWORD";

    public override string Message { get; } = "密码错误";

    public WrongPasswordException()
    {
    }

    public WrongPasswordException(string message) : base(message)
    {
        this.Message = message;
    }

    public WrongPasswordException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public WrongPasswordException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public WrongPasswordException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}