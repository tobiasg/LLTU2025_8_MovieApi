using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movies.Core.Entities;

namespace Movies.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews", table => table.HasCheckConstraint("CK_Reviews_Rating", "Rating >= 1.0 AND Rating <= 5.0"));

        builder.HasKey(movie => movie.Id);

        builder.Property(movie => movie.CreatedAt).IsRequired();
        builder.Property(movie => movie.UpdatedAt).IsRequired();
        builder.Property(movie => movie.DeletedAt);

        builder.Property(movie => movie.Name).IsRequired().HasMaxLength(255);
        builder.Property(movie => movie.Comment).HasMaxLength(1000).IsRequired();
        builder.Property(r => r.Rating).IsRequired().HasPrecision(2, 1);
    }
}
