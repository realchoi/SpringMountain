namespace SpringMountain.Framework.Core.Domain.Entities;

/// <summary>
/// 实体接口
/// </summary>
public interface IEntity
{
}

/// <summary>
/// 实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}