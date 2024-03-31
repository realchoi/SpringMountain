using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 租户未找到异常（404）
/// </summary>
public class TenantNotFoundException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.NotFound;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.TenantNotFound;

    public override string Status { get; } = "TENANT_NOT_FOUND";

    public override string Message { get; } = "租户未找到";

    public TenantNotFoundException()
    {
    }

    public TenantNotFoundException(string message) : base(message)
    {
        this.Message = message;
    }

    public TenantNotFoundException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public TenantNotFoundException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public TenantNotFoundException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}