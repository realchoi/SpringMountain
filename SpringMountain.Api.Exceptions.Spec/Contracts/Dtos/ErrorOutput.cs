using System.Collections;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 自定义错误信息
/// </summary>
public class ErrorOutput
{
    /// <summary>
    /// 初始化自定义错误信息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="status"></param>
    /// <param name="message"></param>
    public ErrorOutput(InternalErrorCode code, string status, string message)
    {
        Code = code;
        Status = status;
        Message = message;
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public InternalErrorCode Code { get; private set; }

    /// <summary>
    /// 状态描述
    /// </summary>
    public string Status { get; private set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// 错误详情
    /// </summary>
    public IEnumerable? Details { get; set; }
}