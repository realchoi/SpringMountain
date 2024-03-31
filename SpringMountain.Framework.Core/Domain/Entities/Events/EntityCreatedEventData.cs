namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体创建事件数据（保存数据库之后触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中创建的实体</param>
    public EntityCreatedEventData(TEntity entity) : base(entity)
    {
    }
}