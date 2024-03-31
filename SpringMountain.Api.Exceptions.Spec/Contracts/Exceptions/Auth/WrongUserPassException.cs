using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 用户名或密码错误异常（401）
/// </summary>
public class WrongUserPassException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.Unauthorized;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.WrongUserPass;

    public override string Status { get; } = "WRONG_USERPASS";

    public override string Message { get; } = "用户名或密码错误";

    public WrongUserPassException()
    {
    }

    public WrongUserPassException(string message) : base(message)
    {
        this.Message = message;
    }

    public WrongUserPassException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public WrongUserPassException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public WrongUserPassException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}