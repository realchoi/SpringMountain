using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SpringMountain.Framework.Core.Domain.Auditing;
using SpringMountain.Framework.Core.Domain.Entities;
using SpringMountain.Framework.Core.Domain.Entities.Events;
using SpringMountain.Framework.Core.Domain.Events;
using SpringMountain.Infrastructure.Snowflake;

namespace SpringMountain.Framework.EfCore.Domain.Repositories;

/// <summary>
/// 核心数据库上下文类
/// </summary>
public abstract class CoreDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected CoreDbContext(DbContextOptions options) : base(options)
    {
        var serviceProvider = options.FindExtension<CoreOptionsExtension>()?.ApplicationServiceProvider;
        _mediator = serviceProvider!.GetRequiredService<IMediator>();
        _httpContextAccessor = serviceProvider!.GetRequiredService<IHttpContextAccessor>();
    }

    /// <summary>
    /// 保存实体到数据库
    /// </summary>
    /// <remarks>
    /// 内部逻辑顺序：1. 先发布实体变更前的领域事件；2. 实际落库；3. 再发布实体变更后的领域事件。
    /// </remarks>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <returns></returns>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AcceptAllChanges();
        // 先发布实体变更前的领域事件
        PublishChangingEventData(CancellationToken.None).GetAwaiter().GetResult();
        // 落库
        var num = base.SaveChanges(acceptAllChangesOnSuccess);
        // 再发布实体变更后的领域事件
        PublishEventData(CancellationToken.None).GetAwaiter().GetResult();
        return num;
    }

    /// <summary>
    /// 保存实体到数据库（异步）
    /// </summary>
    /// <remarks>
    /// 内部逻辑顺序：1. 先发布实体变更前的领域事件；2. 实际落库；3. 再发布实体变更后的领域事件。
    /// </remarks>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        AcceptAllChanges();
        // 先发布实体变更前的领域事件
        await PublishChangingEventData(cancellationToken);
        // 落库
        var num = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        // 再发布实体变更后的领域事件
        await PublishEventData(cancellationToken);
        return num;
    }

    /// <summary>
    /// 发布实体变更前的领域事件
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task PublishChangingEventData(CancellationToken cancellationToken)
    {
        var changing = _entityChangeEvents.Where(c => c is IEntityChangingEventData).ToList();
        _entityChangeEvents.RemoveAll(changing);
        if (changing.Any())
        {
            var copy = new object[_entityChangeEvents.Count];
            changing.CopyTo(copy);
            changing.Clear();
            foreach (var entityChangeEvent in copy.Where(c => c != null))
            {
                await _mediator.Publish(entityChangeEvent, cancellationToken);
            }
        }
    }

    /// <summary>
    /// 发布实体领域事件
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task PublishEventData(CancellationToken cancellationToken)
    {
        var events = GetDomainEvents();
        if (_mediator == null)
            return;
        foreach (var @event in events)
        {
            await _mediator.Publish(@event, cancellationToken);
        }

        var copy = new object[_entityChangeEvents.Count];
        _entityChangeEvents.CopyTo(copy);
        _entityChangeEvents.Clear();
        foreach (var entityChangeEvent in copy.Where(c => c != null))
        {
            await _mediator.Publish(entityChangeEvent, cancellationToken);
        }
    }

    /// <summary>
    /// 获取当前变更的聚合根的领域事件
    /// </summary>
    /// <returns></returns>
    protected virtual List<AggregateRootEvent> GetDomainEvents()
    {
        var events = new List<AggregateRootEvent>();
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry.Entity is not AggregateRoot domainEntity) continue;
            var domainEvents = domainEntity.GetEvents().ToList();
            if (!domainEvents.Any()) continue;
            events.AddRange(domainEvents);
            domainEntity.ClearEvents();
        }

        return events;
    }

    private readonly List<object> _entityChangeEvents = new List<object>();

    /// <summary>
    /// 接受所有的实体更改
    /// </summary>
    private void AcceptAllChanges()
    {
        var entries = base.ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            Type? changed = null;
            Type? changing = null;
            switch (entry.State)
            {
                // 实体新增
                case EntityState.Added:
                    // 如果主键不存在，则自动生成
                    if (entry.Entity is Entity<long> longEntity && longEntity.Id <= 0)
                    {
                        longEntity.Id = IdGenerator.NextId();
                    }

                    // 自动给创建时间赋值
                    if (entry.Entity is IHasCreationTime creationTime && creationTime.CreationTime == default)
                    {
                        creationTime.CreationTime = DateTime.Now;
                    }

                    // 自动给创建用户赋值
                    if (entry.Entity is IHasCreationUser creationUser)
                    {
                        creationUser.CreationUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                    }

                    // 自动给修改时间赋值
                    if (entry.Entity is IHasModificationTime modificationTime &&
                        modificationTime.ModificationTime == default)
                    {
                        modificationTime.ModificationTime = DateTime.Now;
                    }

                    changed = typeof(EntityCreatedEventData<>).MakeGenericType(entry.Entity.GetType());
                    changing = typeof(EntityCreatingEventData<>).MakeGenericType(entry.Entity.GetType());
                    break;
                // 实体修改
                case EntityState.Modified:
                    // 自动给修改时间赋值
                    if (entry.Entity is IHasModificationTime modificationTime2)
                    {
                        modificationTime2.ModificationTime = DateTime.Now;
                    }

                    // 自动给修改用户赋值
                    if (entry.Entity is IHasModificationUser modificationUser)
                    {
                        modificationUser.ModificationUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                    }

                    changed = typeof(EntityUpdatedEventData<>).MakeGenericType(entry.Entity.GetType());
                    changing = typeof(EntityUpdatingEventData<>).MakeGenericType(entry.Entity.GetType());
                    break;
                // 实体删除
                case EntityState.Deleted:
                    if (entry.Entity is ISoftDeleted softDeleted)
                    {
                        softDeleted.IsDeleted = true;
                        if (entry.Entity is IHasModificationUser delEntity)
                        {
                            // 自动给修改用户赋值
                            delEntity.ModificationUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                        }

                        entry.State = EntityState.Modified;
                    }

                    changed = typeof(EntityDeletedEventData<>).MakeGenericType(entry.Entity.GetType());
                    changing = typeof(EntityDeletingEventData<>).MakeGenericType(entry.Entity.GetType());
                    break;
                default:
                    changed = null;
                    break;
            }

            if (changed != null)
            {
                _entityChangeEvents.Add(Activator.CreateInstance(changed, entry.Entity));
            }

            if (changing != null)
            {
                _entityChangeEvents.Add(Activator.CreateInstance(changing, entry.Entity));
            }
        }
    }
}