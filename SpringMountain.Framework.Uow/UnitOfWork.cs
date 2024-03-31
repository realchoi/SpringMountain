using System.Collections.Immutable;
using SpringMountain.Api.Exceptions.Contracts;

namespace SpringMountain.Framework.Uow;

/// <summary>
/// 工作单元
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        _databaseApis = new Dictionary<string, IDatabaseApi>();
        _transactionApis = new Dictionary<string, ITransactionApi>();
        Options = new UnitOfWorkOptions();
    }

    public Guid Id { get; } = Guid.NewGuid();

    public UnitOfWorkOptions Options { get; private set; }

    public IUnitOfWork? Outer { get; private set; }

    public bool IsDisposed { get; private set; }

    public bool IsCompleted { get; private set; }

    protected List<Func<Task>> CompletedHandlers { get; } = new List<Func<Task>>();

    public event EventHandler<UnitOfWorkFailedEventArgs>? Failed;

    public event EventHandler<UnitOfWorkEventArgs>? Disposed;

    public IServiceProvider ServiceProvider { get; }

    private readonly Dictionary<string, IDatabaseApi> _databaseApis;
    private readonly Dictionary<string, ITransactionApi> _transactionApis;

    private Exception? _exception;
    private bool _isCompleting;
    private bool _isRolledback;

    public virtual void SetOuter(IUnitOfWork? outer)
    {
        Outer = outer;
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_isRolledback)
        {
            return;
        }

        foreach (var databaseApi in GetAllActiveDatabaseApis())
        {
            if (databaseApi is ISupportsSavingChanges database)
            {
                await database.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public virtual async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        if (_isRolledback)
        {
            return;
        }

        PreventMultipleComplete();

        try
        {
            _isCompleting = true;

            await SaveChangesAsync(cancellationToken);
            await CommitTransactionsAsync();

            IsCompleted = true;
            await OnCompletedAsync();
        }
        catch (Exception ex)
        {
            _exception = ex;
            throw;
        }
    }

    public virtual async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_isRolledback)
        {
            return;
        }

        _isRolledback = true;

        await RollbackAllAsync(cancellationToken);
    }

    public void OnCompleted(Func<Task> handler)
    {
    }

    private void PreventMultipleComplete()
    {
        if (IsCompleted || _isCompleting)
        {
            throw new ApiBaseException("Complete is called before!");
        }
    }

    protected virtual async Task RollbackAllAsync(CancellationToken cancellationToken)
    {
        foreach (var databaseApi in GetAllActiveDatabaseApis())
        {
            if (databaseApi is ISupportsRollback rollback)
            {
                try
                {
                    await rollback.RollbackAsync(cancellationToken);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }

        foreach (var transactionApi in GetAllActiveTransactionApis())
        {
            if (transactionApi is ISupportsRollback rollback)
            {
                try
                {
                    await rollback.RollbackAsync(cancellationToken);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }
    }

    protected virtual async Task CommitTransactionsAsync()
    {
        foreach (var transaction in GetAllActiveTransactionApis())
        {
            await transaction.CommitAsync();
        }
    }

    protected virtual async Task OnCompletedAsync()
    {
        foreach (var handler in CompletedHandlers)
        {
            await handler.Invoke();
        }
    }

    public virtual void AddDatabaseApi(string key, IDatabaseApi api)
    {
        if (_databaseApis.ContainsKey(key))
        {
            throw new ApiBaseException("There is already a database API in this unit of work with given key: " + key);
        }

        _databaseApis.Add(key, api);
    }

    public virtual IDatabaseApi GetOrAddDatabaseApi(string key, Func<IDatabaseApi> factory)
    {
        return _databaseApis.GetOrAdd(key, factory);
    }

    public virtual void AddTransactionApi(string key, ITransactionApi api)
    {
        if (_transactionApis.ContainsKey(key))
        {
            throw new ApiBaseException("There is already a transaction API in this unit of work with given key: " +
                                       key);
        }

        _transactionApis.Add(key, api);
    }

    public virtual ITransactionApi GetOrAddTransactionApi(string key, Func<ITransactionApi> factory)
    {
        return _transactionApis.GetOrAdd(key, factory);
    }

    public virtual IReadOnlyList<IDatabaseApi> GetAllActiveDatabaseApis()
    {
        return _databaseApis.Values.ToImmutableList();
    }

    public virtual IReadOnlyList<ITransactionApi> GetAllActiveTransactionApis()
    {
        return _transactionApis.Values.ToImmutableList();
    }

    public void Initialize(UnitOfWorkOptions options)
    {
        Options = options.Clone();
    }

    public IDatabaseApi? FindDatabaseApi(string key)
    {
        return _databaseApis.GetOrDefault(key);
    }

    public ITransactionApi? FindTransactionApi(string key)
    {
        return _transactionApis.GetOrDefault(key);
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    private void DisposeTransactions()
    {
        foreach (var transactionApi in GetAllActiveTransactionApis())
        {
            try
            {
                transactionApi.Dispose();
            }
            catch
            {
            }
        }
    }

    protected virtual void OnFailed()
    {
        Failed?.Invoke(this, new UnitOfWorkFailedEventArgs(this, _exception!, _isRolledback));
    }

    protected virtual void OnDisposed()
    {
        Disposed?.Invoke(this, new UnitOfWorkEventArgs(this));
    }

    public void Dispose()
    {
        //必须为true
        Dispose(true);
        //通知垃圾回收器不再调用终结器
        //GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;

        DisposeTransactions();

        if (!IsCompleted || _exception != null)
        {
            OnFailed();
        }

        OnDisposed();
    }
}