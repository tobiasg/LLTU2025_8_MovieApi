using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Data.Configurations;

namespace Movies.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; } = default!;
    public DbSet<Actor> Actors { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new MovieDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new ActorConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());

        modelBuilder.Entity<Movie>().HasQueryFilter(movie => movie.DeletedAt == null);
        modelBuilder.Entity<MovieDetails>().HasQueryFilter(details => details.DeletedAt == null);
        modelBuilder.Entity<Actor>().HasQueryFilter(actor => actor.DeletedAt == null);
        modelBuilder.Entity<Review>().HasQueryFilter(review => review.DeletedAt == null);
        modelBuilder.Entity<Genre>().HasQueryFilter(genre => genre.DeletedAt == null);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
