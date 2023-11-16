using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.ReportAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Context;
public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Report> Reports { get; set; } = default!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
