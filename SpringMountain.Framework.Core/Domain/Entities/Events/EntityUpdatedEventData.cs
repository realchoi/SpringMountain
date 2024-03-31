namespace SpringMountain.Framework.Core.Domain.Entities.Events;

/// <summary>
/// 实体更新事件数据（保存数据库之后触发）
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="entity">当前事件中更新的实体</param>
    public EntityUpdatedEventData(TEntity entity) : base(entity)
    {
    }
}