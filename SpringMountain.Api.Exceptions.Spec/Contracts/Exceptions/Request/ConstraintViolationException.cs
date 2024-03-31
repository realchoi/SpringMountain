using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 违反业务约束异常（400）
/// </summary>
public class ConstraintViolationException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.BadRequest;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.ConstraintViolation;

    public override string Status { get; } = "CONSTRAINT_VIOLATION";

    public override string Message { get; } = "违反业务约束";

    public ConstraintViolationException()
    {
    }

    public ConstraintViolationException(string message) : base(message)
    {
        this.Message = message;
    }

    public ConstraintViolationException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public ConstraintViolationException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public ConstraintViolationException(string message, IEnumerable details, Exception exception)
        : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}