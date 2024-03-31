namespace SpringMountain.Framework.Core.Domain.Entities;

/// <summary>
/// 实体基类
/// </summary>
public class Entity : IEntity
{
}

public class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; set; }
}