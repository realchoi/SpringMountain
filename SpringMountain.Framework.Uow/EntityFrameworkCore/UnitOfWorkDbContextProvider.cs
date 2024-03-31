using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SpringMountain.Api.Exceptions.Contracts;

namespace SpringMountain.Framework.Uow.EntityFrameworkCore;

/// <summary>
/// 数据库上下文对象提供器类
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// 工作单元管理器
    /// </summary>
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWorkDbContextProvider{TDbContext}" /> class.
    /// </summary>
    /// <param name="unitOfWorkManager">工作单元管理器</param>
    public UnitOfWorkDbContextProvider(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    /// <summary>
    /// 获取数据库上下文对象
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApiBaseException"></exception>
    public async Task<TDbContext> GetDbContextAsync()
    {
        var unitOfWork = _unitOfWorkManager.Current;
        if (unitOfWork == null)
        {
            throw new ApiBaseException("A DbContext can only be created inside a unit of work!");
        }

        var databaseApi = unitOfWork.FindDatabaseApi(typeof(TDbContext).Name);

        if (databaseApi == null)
        {
            databaseApi = new EfCoreDatabaseApi(await CreateDbContextAsync(unitOfWork));

            unitOfWork.AddDatabaseApi(typeof(TDbContext).Name, databaseApi);
        }

        return (TDbContext)((EfCoreDatabaseApi)databaseApi).DbContext;
    }

    /// <summary>
    /// 创建数据库上下文对象
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <returns></returns>
    private async Task<TDbContext> CreateDbContextAsync(IUnitOfWork unitOfWork)
    {
        var dbContext = unitOfWork.Options.IsTransactional
            ? await CreateDbContextWithTransactionAsync(unitOfWork)
            : unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();

        if (unitOfWork.Options.Timeout.HasValue &&
            dbContext.Database.IsRelational() &&
            !dbContext.Database.GetCommandTimeout().HasValue)
        {
            dbContext.Database.SetCommandTimeout(TimeSpan.FromMilliseconds(unitOfWork.Options.Timeout.Value));
        }

        return dbContext;
    }

    /// <summary>
    /// 创建具有事务的数据库上下文对象
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <returns></returns>
    private async Task<TDbContext> CreateDbContextWithTransactionAsync(IUnitOfWork unitOfWork)
    {
        var transactionApiKey = typeof(TDbContext).Name;
        var activeTransaction = unitOfWork.FindTransactionApi(transactionApiKey) as EfCoreTransactionApi;

        if (activeTransaction == null)
        {
            var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();

            var dbTransaction = unitOfWork.Options.IsolationLevel.HasValue
                ? await dbContext.Database.BeginTransactionAsync(unitOfWork.Options.IsolationLevel.Value)
                : await dbContext.Database.BeginTransactionAsync();
            {
#pragma warning disable CA2000 // 丢失范围之前释放对象
                var efCoreTransactionAp = new EfCoreTransactionApi(dbTransaction,
                    dbContext);
#pragma warning restore CA2000 // 丢失范围之前释放对象

                unitOfWork.AddTransactionApi(
                    transactionApiKey,
                    efCoreTransactionAp);

                return dbContext;
            }
        }
        else
        {
            var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();

            if (dbContext.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager)
            {
                if (dbContext.Database.GetDbConnection() ==
                    activeTransaction.DbContextTransaction.GetDbTransaction().Connection)
                {
                    await dbContext.Database.UseTransactionAsync(
                        activeTransaction.DbContextTransaction.GetDbTransaction());
                }
                else
                {
                    /* User did not re-use the ExistingConnection and we are starting a new transaction.
                     * EfCoreTransactionApi will check the connection string match and separately
                     * commit/rollback this transaction over the DbContext instance. */
                    if (unitOfWork.Options.IsolationLevel.HasValue)
                    {
                        await dbContext.Database.BeginTransactionAsync(
                            unitOfWork.Options.IsolationLevel.Value
                        );
                    }
                    else
                    {
                        await dbContext.Database.BeginTransactionAsync();
                    }
                }
            }
            else
            {
                await dbContext.Database.BeginTransactionAsync();
            }

            activeTransaction.AttendedDbContexts.Add(dbContext);
            return dbContext;
        }
    }
}