﻿using Microsoft.EntityFrameworkCore;

namespace SpringMountain.Framework.Uow.EntityFrameworkCore;

public class EfCoreDatabaseApi : IDatabaseApi, ISupportsSavingChanges
{
    public DbContext DbContext { get; }

    public EfCoreDatabaseApi(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}