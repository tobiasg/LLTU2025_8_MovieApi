using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.Entities;

namespace Movies.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(movie => movie.Id);

        builder.Property(movie => movie.CreatedAt).IsRequired();
        builder.Property(movie => movie.UpdatedAt).IsRequired();
        builder.Property(movie => movie.DeletedAt);

        builder.Property(movie => movie.Title).IsRequired().HasMaxLength(255);
        builder.Property(movie => movie.Year).IsRequired();
        builder.Property(movie => movie.Duration).IsRequired();
    }
}
