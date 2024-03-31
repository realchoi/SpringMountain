namespace SpringMountain.Framework.Uow;

internal class ChildUnitOfWork : IUnitOfWork
{
    private readonly IUnitOfWork _parent;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public ChildUnitOfWork(IUnitOfWork parent)
    {
        _parent = parent;
    }

    public Guid Id => _parent.Id;
    public IServiceProvider ServiceProvider => _parent.ServiceProvider;

    public UnitOfWorkOptions Options => _parent.Options;
    public IUnitOfWork? Outer => _parent.Outer;

    public bool IsDisposed => _parent.IsDisposed;

    public bool IsCompleted => _parent.IsCompleted;

    public event EventHandler<UnitOfWorkFailedEventArgs>? Failed;
    public event EventHandler<UnitOfWorkEventArgs>? Disposed;

    public void SetOuter(IUnitOfWork? outer)
    {
        _parent.SetOuter(outer);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _parent.SaveChangesAsync(cancellationToken);
    }

    public Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        return _parent.RollbackAsync(cancellationToken);
    }

    public void OnCompleted(Func<Task> handler)
    {
        _parent.OnCompleted(handler);
    }

    public void AddDatabaseApi(string key, IDatabaseApi? api)
    {
        _parent.AddDatabaseApi(key, api);
    }

    public IDatabaseApi GetOrAddDatabaseApi(string key, Func<IDatabaseApi> factory)
    {
        return _parent.GetOrAddDatabaseApi(key, factory);
    }

    public void AddTransactionApi(string key, ITransactionApi? api)
    {
        _parent.AddTransactionApi(key, api);
    }

    public ITransactionApi GetOrAddTransactionApi(string key, Func<ITransactionApi> factory)
    {
        return _parent.GetOrAddTransactionApi(key, factory);
    }

    public IReadOnlyList<IDatabaseApi> GetAllActiveDatabaseApis()
    {
        return _parent.GetAllActiveDatabaseApis();
    }

    public IReadOnlyList<ITransactionApi> GetAllActiveTransactionApis()
    {
        return _parent.GetAllActiveTransactionApis();
    }

    public void Initialize(UnitOfWorkOptions options)
    {
        _parent.Initialize(options);
    }

    public IDatabaseApi? FindDatabaseApi(string key)
    {
        return _parent.FindDatabaseApi(key);
    }

    public ITransactionApi? FindTransactionApi(string key)
    {
        return _parent.FindTransactionApi(key);
    }

    public void Dispose()
    {
    }

    public override string ToString()
    {
        return $"[UnitOfWork {Id}]";
    }
}