using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 服务维护中异常（503）
/// </summary>
public class UnderMaintenanceException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.ServiceUnavailable;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.UnderMaintenance;

    public override string Status { get; } = "UNDER_MAINTENANCE";

    public override string Message { get; } = "服务维护中";

    public UnderMaintenanceException()
    {
    }

    public UnderMaintenanceException(string message) : base(message)
    {
        this.Message = message;
    }

    public UnderMaintenanceException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public UnderMaintenanceException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public UnderMaintenanceException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}