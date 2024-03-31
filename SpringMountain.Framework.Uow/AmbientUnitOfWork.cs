namespace SpringMountain.Framework.Uow;

/// <summary>
/// 基于环境流动的工作单元
/// </summary>
public class AmbientUnitOfWork : IAmbientUnitOfWork
{
    public IUnitOfWork? UnitOfWork => _currentUow.Value;

    private readonly AsyncLocal<IUnitOfWork?> _currentUow;

    public AmbientUnitOfWork()
    {
        _currentUow = new AsyncLocal<IUnitOfWork?>();
    }

    public void SetUnitOfWork(IUnitOfWork? unitOfWork)
    {
        _currentUow.Value = unitOfWork;
    }

    public IUnitOfWork? GetCurrentByChecking()
    {
        var uow = UnitOfWork;

        // Skip reserved unit of work
        while (uow != null && (uow.IsDisposed || uow.IsCompleted))
        {
            uow = uow.Outer;
        }

        return uow;
    }
}