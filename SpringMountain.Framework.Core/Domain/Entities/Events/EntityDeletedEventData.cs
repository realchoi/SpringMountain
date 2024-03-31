namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体删除事件数据（保存数据库之后触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中删除的实体</param>
    public EntityDeletedEventData(TEntity entity) : base(entity)
    {
    }
}