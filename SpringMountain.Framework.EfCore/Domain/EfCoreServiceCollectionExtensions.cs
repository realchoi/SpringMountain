using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpringMountain.Framework.Core.Domain.Entities;
using SpringMountain.Framework.Core.Domain.Repositories;
using System.Reflection;
using SpringMountain.Framework.EfCore.Domain.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// EfCore 依赖注入扩展
/// </summary>
public static class EfCoreServiceCollectionExtensions
{
    /// <summary>
    /// 注入数据库上下文对象和仓储对象。
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文类型</typeparam>
    /// <param name="services">服务容器</param>
    /// <param name="optionsAction">数据库上下文配置工厂</param>
    /// <returns></returns>
    public static IServiceCollection AddDbContextAndEfRepositories<TDbContext>(this IServiceCollection services,
        Action<DbContextOptionsBuilder>? optionsAction = null) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(optionsAction)
            .AddRepositories<TDbContext>();
        return services;
    }

    /// <summary>
    /// 注入仓储对象。
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文类型</typeparam>
    /// <param name="services">服务容器</param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        return services.AddRepositories(typeof(TDbContext));
    }

    /// <summary>
    /// 注入仓储对象。
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="dbContextType">数据库上下文类型</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddRepositories(this IServiceCollection services, Type dbContextType)
    {
        if (!typeof(DbContext).IsAssignableFrom(dbContextType))
            throw new ArgumentException($"参数 {nameof(dbContextType)} 的类型错误，该类型必须继承自 [{nameof(DbContext)}]");

        var entityTypes = GetEntityTypes(dbContextType);
        foreach (var entityType in entityTypes)
        {
            services.AddRepositories(entityType, dbContextType);
        }

        return services;
    }

    /// <summary>
    /// 注入仓储对象。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContext">数据库上下文类型</typeparam>
    /// <param name="services">服务容器</param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories<TEntity, TDbContext>(this IServiceCollection services)
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        return services.AddRepositories(typeof(TEntity), typeof(TDbContext));
    }

    /// <summary>
    /// 注入仓储对象
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="entityType">实体类型</param>
    /// <param name="dbContextType">数据库上下文类型</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IServiceCollection AddRepositories(this IServiceCollection services, Type entityType,
        Type dbContextType)
    {
        if (!typeof(IEntity).IsAssignableFrom(entityType))
        {
            throw new ArgumentException($"参数 {nameof(entityType)} 的类型错误，该类型必须继承自 [{nameof(IEntity)}]");
        }

        if (!typeof(DbContext).IsAssignableFrom(dbContextType))
        {
            throw new ArgumentException($"参数 {nameof(dbContextType)} 的类型错误，该类型必须继承自 [{nameof(DbContext)}]");
        }

        if (entityType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntity<>)))
        {
            var iKey = entityType.GetInterfaces()
                .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntity<>))
                .GenericTypeArguments.Single();
            var repositoryType = typeof(IRepository<,>).MakeGenericType(entityType, iKey);
            var efCoreRepositoryType = typeof(EfCoreRepository<,,>).MakeGenericType(dbContextType, entityType, iKey);
            services.TryAddTransient(repositoryType, efCoreRepositoryType);
        }

        var repositoryType1 = typeof(IRepository<>).MakeGenericType(entityType);
        var efCoreRepositoryType1 = typeof(EfCoreRepository<,>).MakeGenericType(dbContextType, entityType);
        services.TryAddTransient(repositoryType1, efCoreRepositoryType1);
        return services;
    }

    /// <summary>
    /// 获取 DbContext 中的实体类。
    /// </summary>
    /// <param name="dbContextType">数据库上下文类型</param>
    /// <returns></returns>
    private static IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return
            from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            where IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) &&
                  typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
            select property.PropertyType.GenericTypeArguments[0];
    }

    /// <summary>
    /// 判断给定的类型，是否是（或继承自）指定的泛型类型。
    /// </summary>
    /// <param name="givenType">需要判断的类型</param>
    /// <param name="genericType">泛型类型</param>
    /// <returns></returns>
    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var givenTypeInfo = givenType.GetTypeInfo();
        if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        foreach (var interfaceType in givenTypeInfo.GetInterfaces())
        {
            if (interfaceType.GetTypeInfo().IsGenericType
                && interfaceType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
        }

        if (givenTypeInfo.BaseType == null)
        {
            return false;
        }

        return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
    }
}