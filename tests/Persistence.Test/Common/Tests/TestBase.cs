using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Infrastructure.Context;
using Persistence.Contracts.Repositories;
using Persistence.Test.Common.Host;

namespace Persistence.Test.Common.Tests;

public abstract class TestBase
{
    private readonly App _app = App.Create();
    protected ApplicationDbContext DbContext => _app.DbContext;
    protected IRepository<Account, AccountId> AccountRepository =>
        _app.GetRequiredService<IRepository<Account, AccountId>>();

    protected IRepository<Report, ReportId> ReportRepository =>
        _app.GetRequiredService<IRepository<Report, ReportId>>();

    protected virtual async Task InitializeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    protected virtual async Task CleanupAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        _app.Dispose();
    }

    protected async Task SaveAndClearChangeTrackerAsync(){
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
    }
}
