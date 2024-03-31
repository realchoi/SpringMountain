namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体创建事件数据（保存数据库之前触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中创建的实体</param>
    public EntityCreatingEventData(TEntity entity) : base(entity)
    {
    }
}