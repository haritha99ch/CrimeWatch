using Application.Test.Common.Host;
using Infrastructure.Context;
using MediatR;

namespace Application.Test.Common.Tests;

public abstract class TestBase
{
    private readonly App _app = App.Create();
    protected ApplicationDbContext DbContext => _app.DbContext;

    protected ISender Mediator => _app.GetRequiredService<ISender>();

    protected virtual async Task InitializeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    protected virtual async Task CleanupAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    protected async Task SaveAndClearChangeTrackerAsync()
    {
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
    }
}
