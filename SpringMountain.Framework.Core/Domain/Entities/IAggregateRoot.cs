namespace SpringMountain.Framework.Core.Domain.Entities;

/// <summary>
/// 聚合根接口
/// </summary>
public interface IAggregateRoot : IEntity
{
}

/// <summary>
/// 聚合根接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
{
}