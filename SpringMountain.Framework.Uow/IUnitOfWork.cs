namespace SpringMountain.Framework.Uow;

/// <summary>
/// 工作单元接口
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Guid Id { get; }

    IServiceProvider ServiceProvider { get; }

    event EventHandler<UnitOfWorkFailedEventArgs> Failed;

    event EventHandler<UnitOfWorkEventArgs> Disposed;

    UnitOfWorkOptions Options { get; }

    IUnitOfWork? Outer { get; }

    bool IsDisposed { get; }

    bool IsCompleted { get; }

    void SetOuter(IUnitOfWork? outer);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task CompleteAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancelToken = default);

    void OnCompleted(Func<Task> handler);

    void AddDatabaseApi(string key, IDatabaseApi api);

    IDatabaseApi GetOrAddDatabaseApi(string key, Func<IDatabaseApi> factory);

    void AddTransactionApi(string key, ITransactionApi api);

    ITransactionApi GetOrAddTransactionApi(string key, Func<ITransactionApi> factory);

    IReadOnlyList<IDatabaseApi> GetAllActiveDatabaseApis();

    IReadOnlyList<ITransactionApi> GetAllActiveTransactionApis();

    void Initialize(UnitOfWorkOptions options);

    IDatabaseApi? FindDatabaseApi(string key);

    ITransactionApi? FindTransactionApi(string key);
}