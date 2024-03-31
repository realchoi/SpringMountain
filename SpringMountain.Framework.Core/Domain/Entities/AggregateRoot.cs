using SpringMountain.Framework.Core.Domain.Events;

namespace SpringMountain.Framework.Core.Domain.Entities;

/// <summary>
/// 聚合根基类
/// </summary>
public class AggregateRoot : Entity, IAggregateRoot
{
    /// <summary>
    /// 聚合根事件集合
    /// </summary>
    private readonly ICollection<AggregateRootEvent> _events = new List<AggregateRootEvent>();

    /// <summary>
    /// 添加聚合根事件
    /// </summary>
    /// <param name="event"></param>
    public void AddEvent(AggregateRootEvent @event) => _events.Add(@event);

    /// <summary>
    /// 获取聚合根事件集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<AggregateRootEvent> GetEvents() => _events;

    /// <summary>
    /// 清空聚合根事件
    /// </summary>
    public void ClearEvents() => _events.Clear();
}

/// <summary>
/// 聚合根基类
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class AggregateRoot<TKey> : AggregateRoot, IAggregateRoot<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; set; }
}