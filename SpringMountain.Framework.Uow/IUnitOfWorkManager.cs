namespace SpringMountain.Framework.Uow;

public interface IUnitOfWorkManager
{
    IUnitOfWork? Current { get; }

    IUnitOfWork Begin(UnitOfWorkOptions options, bool requiresNew = false);
}