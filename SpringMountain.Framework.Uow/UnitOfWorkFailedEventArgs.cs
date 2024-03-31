namespace SpringMountain.Framework.Uow;

public class UnitOfWorkFailedEventArgs : UnitOfWorkEventArgs
{
    public Exception Exception { get; }

    public bool IsRolledBack { get; }

    public UnitOfWorkFailedEventArgs(IUnitOfWork unitOfWork, Exception exception, bool isRolledBack) : base(unitOfWork)
    {
        Exception = exception;
        IsRolledBack = isRolledBack;
    }
}