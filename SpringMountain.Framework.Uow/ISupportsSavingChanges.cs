namespace SpringMountain.Framework.Uow;

public interface ISupportsSavingChanges
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}