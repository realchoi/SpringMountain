using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 请求异常（400）
/// </summary>
public class BadRequestException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.BadRequest;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.BadRequest;

    public override string Status { get; } = "BAD_REQUEST";

    public override string Message { get; } = "请求异常";

    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
        this.Message = message;
    }

    public BadRequestException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public BadRequestException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public BadRequestException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}