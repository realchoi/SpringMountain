using MediatR;

namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体事件数据基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityEventData<TEntity> : INotification
{
    /// <summary>
    /// 当前事件关联的实体
    /// </summary>
    public TEntity Entity { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity"></param>
    public EntityEventData(TEntity entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// 创建实体事件数据
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="entityEventType"></param>
    /// <returns></returns>
    public static object? CreateEntityEventData(object entity, Type entityEventType)
    {
        var type = entityEventType.MakeGenericType(entity.GetType());
        return Activator.CreateInstance(type, entity);
    }
}