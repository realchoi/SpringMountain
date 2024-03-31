using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 遗漏参数异常（400）
/// </summary>
public class MissingParameterException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.BadRequest;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.MissingParameter;

    public override string Status { get; } = "MISSING_PARAMETER";

    public override string Message { get; } = "遗漏参数";

    public MissingParameterException()
    {
    }

    public MissingParameterException(string message) : base(message)
    {
        this.Message = message;
    }

    public MissingParameterException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public MissingParameterException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public MissingParameterException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}