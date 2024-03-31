using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 远程过程调用失败异常（500）
/// </summary>
public class RpcFailedException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.RpcFailed;

    public string ServiceName { get; set; } = "";

    public string ServiceVersion { get; set; } = "";

    public override string Status { get; } = "RPC_FAILED";

    public override string Message { get; } = "远程过程调用失败";

    public RpcFailedException()
    {
    }

    public RpcFailedException(string message) : base(message)
    {
        this.Message = message;
    }

    public RpcFailedException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public RpcFailedException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public RpcFailedException(string message, IEnumerable details, Exception exception) : base(message, exception)
    {
        this.Message = message;
        base.Details = details;
    }
}