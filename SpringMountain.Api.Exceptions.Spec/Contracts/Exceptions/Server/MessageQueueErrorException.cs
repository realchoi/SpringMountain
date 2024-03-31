using System.Collections;
using System.Net;

namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 消息队列错误异常（500）
/// </summary>
public class MessageQueueErrorException : ApiBaseException
{
    public override HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;

    public override InternalErrorCode ErrorCode { get; set; } = InternalErrorCode.MessageQueueError;

    public override string Status { get; } = "MESSAGE_QUEUE_ERROR";

    public override string Message { get; } = "消息队列错误";

    public MessageQueueErrorException()
    {
    }

    public MessageQueueErrorException(string message) : base(message)
    {
        this.Message = message;
    }

    public MessageQueueErrorException(string message, Exception exception) : base(message, exception)
    {
        this.Message = message;
    }

    public MessageQueueErrorException(string message, IEnumerable details) : base(message)
    {
        this.Message = message;
        base.Details = details;
    }

    public MessageQueueErrorException(string message, IEnumerable details, Exception exception) : base(message,
        exception)
    {
        this.Message = message;
        base.Details = details;
    }
}