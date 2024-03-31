namespace SpringMountain.Framework.Uow;

/// <summary>
/// 基于环境流动的工作单元接口
/// </summary>
public interface IAmbientUnitOfWork
{
    IUnitOfWork? UnitOfWork { get; }

    void SetUnitOfWork(IUnitOfWork? unitOfWork);

    IUnitOfWork? GetCurrentByChecking();
}