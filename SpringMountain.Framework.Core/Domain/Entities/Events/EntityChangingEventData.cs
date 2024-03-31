namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体变更事件数据（保存数据库之前触发）
/// </summary>
public class EntityChangingEventData<TEntity> : EntityEventData<TEntity>, IEntityChangingEventData
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中变更的实体</param>
    public EntityChangingEventData(TEntity entity) : base(entity)
    {
    }
}