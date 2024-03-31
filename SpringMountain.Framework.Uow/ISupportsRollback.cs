namespace SpringMountain.Framework.Uow;

public interface ISupportsRollback
{
    Task RollbackAsync(CancellationToken cancellationToken);
}