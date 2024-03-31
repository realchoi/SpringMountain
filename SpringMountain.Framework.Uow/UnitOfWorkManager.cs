using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Framework.Uow;

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IAmbientUnitOfWork _ambientUnitOfWork;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public UnitOfWorkManager(IServiceScopeFactory serviceScopeFactory, IAmbientUnitOfWork ambientUnitOfWork)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _ambientUnitOfWork = ambientUnitOfWork;
    }

    public IUnitOfWork? Current => _ambientUnitOfWork.GetCurrentByChecking();

    public IUnitOfWork Begin(UnitOfWorkOptions options, bool requiresNew = false)
    {
        var currentUow = Current;
        if (currentUow != null && !requiresNew)
        {
            return new ChildUnitOfWork(currentUow);
        }

        var unitOfWork = CreateNewUnitOfWork();
        unitOfWork.Initialize(options);

        return unitOfWork;
    }

    private IUnitOfWork CreateNewUnitOfWork()
    {
        var scope = _serviceScopeFactory.CreateScope();
        try
        {
            var outerUow = _ambientUnitOfWork.UnitOfWork;

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            unitOfWork.SetOuter(outerUow);

            _ambientUnitOfWork.SetUnitOfWork(unitOfWork);

            unitOfWork.Disposed += (sender, args) =>
            {
                _ambientUnitOfWork.SetUnitOfWork(outerUow);
                scope.Dispose();
            };

            return unitOfWork;
        }
        catch
        {
            scope.Dispose();
            throw;
        }
    }
}