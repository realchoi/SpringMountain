using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 缓存不可用异常（500）
/// </summary>
public class CacheUnavailableException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.CacheUnavailable;

    public override string Status { get; } = "CACHE_UNAVAILABLE";

    public override string Message { get; } = "缓存不可用";

    public CacheUnavailableException()
    {
    }

    public CacheUnavailableException(string message) : base(message)
    {
        this.Message = message;
    }

    public CacheUnavailableException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public CacheUnavailableException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public CacheUnavailableException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}