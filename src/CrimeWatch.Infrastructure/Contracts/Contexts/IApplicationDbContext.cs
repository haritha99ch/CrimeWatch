using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrimeWatch.Infrastructure.Contracts.Contexts;
public interface IApplicationDbContext
{
    DbSet<Account> Account { get; set; }
    DbSet<User> User { get; set; }
    DbSet<Witness> Witness { get; set; }
    DbSet<Moderator> Moderator { get; set; }
    DbSet<Report> Report { get; set; }
    DbSet<Evidence> Evidence { get; set; }
    DbSet<MediaItem> MediaItem { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
