namespace SpringMountain.Framework.Uow;

public class UnitOfWorkEventArgs : EventArgs
{
    public IUnitOfWork UnitOfWork { get; }

    public UnitOfWorkEventArgs(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}