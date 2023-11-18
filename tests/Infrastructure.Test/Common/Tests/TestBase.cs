using Infrastructure.Context;
using Infrastructure.Test.Common.Host;

namespace Infrastructure.Test.Common.Tests;
public abstract class TestBase
{
    private readonly App _app = App.Create();
    protected ApplicationDbContext DbContext => _app.DbContext;
}
