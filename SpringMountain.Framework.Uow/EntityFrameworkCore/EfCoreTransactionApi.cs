using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace SpringMountain.Framework.Uow.EntityFrameworkCore;

public class EfCoreTransactionApi : ITransactionApi, ISupportsRollback
{
    public IDbContextTransaction DbContextTransaction { get; }
    public DbContext StarterDbContext { get; }
    public List<DbContext> AttendedDbContexts { get; }

    public EfCoreTransactionApi(
        IDbContextTransaction dbContextTransaction,
        DbContext starterDbContext)
    {
        DbContextTransaction = dbContextTransaction;
        StarterDbContext = starterDbContext;

        AttendedDbContexts = new List<DbContext>();
    }

    public async Task CommitAsync()
    {
        foreach (var dbContext in AttendedDbContexts)
        {
            if (dbContext.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager &&
                dbContext.Database.GetDbConnection() == DbContextTransaction.GetDbTransaction().Connection)
            {
                continue; //Relational databases use the shared transaction if they are using the same connection
            }

            await dbContext.Database.CommitTransactionAsync();
        }

        await DbContextTransaction.CommitAsync();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        foreach (var dbContext in AttendedDbContexts)
        {
            if (dbContext.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager &&
                dbContext.Database.GetDbConnection() == DbContextTransaction.GetDbTransaction().Connection)
            {
                continue; //Relational databases use the shared transaction if they are using the same connection
            }

            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        await DbContextTransaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        //必须为true
        Dispose(true);
        //通知垃圾回收器不再调用终结器
        //GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        DbContextTransaction.Dispose();
    }
}