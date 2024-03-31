using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// API 异常基类（500 服务器内部异常）
/// </summary>
public class ApiBaseException : Exception
{
    /// <summary>
    /// HTTP 状态码（默认 500）
    /// </summary>
    public virtual HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    /// 内部错误码
    /// </summary>
    public virtual InternalErrorCode ErrorCode { get; set; }

    /// <summary>
    /// 状态说明
    /// </summary>
    public virtual string Status { get; } = "INTERNAL_SERVER_ERROR";

    /// <summary>
    /// 异常详情
    /// </summary>
    public IEnumerable Details { get; set; } = new List<string>();

    public ApiBaseException()
    {
    }

    public ApiBaseException(string message)
        : base(message)
    {
    }

    public ApiBaseException(string message, IEnumerable details)
        : base(message)
    {
        Details = details;
    }

    public ApiBaseException(string message, Exception exception)
        : base(message, exception)
    {
    }

    public ApiBaseException(string message, IEnumerable details, Exception exception)
        : base(message, exception)
    {
        Details = details;
    }

    public static ApiBaseException Create(int code, string message)
    {
        if (Enum.TryParse<InternalErrorCode>(code.ToString(), out var result) &&
            Enum.TryParse<HttpStatusCode>((code / 1000).ToString(), out var result2))
        {
            return new ApiBaseException(message)
            {
                ErrorCode = result,
                HttpCode = result2
            };
        }

        return new InternalServerErrorException(message);
    }
}