using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 重复请求异常（400）
/// </summary>
public class DuplicateRequestException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.BadRequest;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.DuplicateRequest;

    public override string Status { get; } = "DUPLICATE_REQUEST";

    public override string Message { get; } = "重复请求";

    public DuplicateRequestException()
    {
    }

    public DuplicateRequestException(string message) : base(message)
    {
        this.Message = message;
    }

    public DuplicateRequestException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public DuplicateRequestException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public DuplicateRequestException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}