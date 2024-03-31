using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 资源已存在异常（400）
/// </summary>
public class AlreadyExistedException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.BadRequest;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.AlreadyExisted;

    public override string Status { get; } = "ALREADY_EXISTED";

    public override string Message { get; } = "资源已存在";

    public AlreadyExistedException()
    {
    }

    public AlreadyExistedException(string message) : base(message)
    {
        this.Message = message;
    }

    public AlreadyExistedException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public AlreadyExistedException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public AlreadyExistedException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}