using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 数据格式不合法异常（500）
/// </summary>
public class InvalidDataException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.InvalidData;

    public override string Status { get; } = "INVALID_DATA";

    public override string Message { get; } = "数据格式不合法";

    public InvalidDataException()
    {
    }

    public InvalidDataException(string message) : base(message)
    {
        this.Message = message;
    }

    public InvalidDataException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public InvalidDataException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public InvalidDataException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}