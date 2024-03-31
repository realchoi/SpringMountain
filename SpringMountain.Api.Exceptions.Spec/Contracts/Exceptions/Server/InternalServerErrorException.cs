using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 内部服务错误异常（500）
/// </summary>
public class InternalServerErrorException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;


    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.InternalServerError;


    public override string Status { get; } = "INTERNAL_SERVER_ERROR";


    public override string Message { get; } = "内部服务错误";


    public InternalServerErrorException()
    {
    }

    public InternalServerErrorException(string message)
        : base(message)
    {
        Message = message;
    }

    public InternalServerErrorException(string message, Exception exception)
        : base(message, exception)
    {
        Message = message;
    }

    public InternalServerErrorException(string message, IEnumerable details)
        : base(message)
    {
        Message = message;
        base.Details = details;
    }

    public InternalServerErrorException(string message, IEnumerable details, Exception exception)
        : base(message, exception)
    {
        Message = message;
        base.Details = details;
    }
}