namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体变更事件数据（保存数据库之后触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中变更的实体</param>
    public EntityChangedEventData(TEntity entity) : base(entity)
    {
    }
}