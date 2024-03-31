namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体删除事件数据（保存数据库之前触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中删除的实体</param>
    public EntityDeletingEventData(TEntity entity) : base(entity)
    {
    }
}