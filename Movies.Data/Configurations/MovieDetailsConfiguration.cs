using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Configurations;

public class MovieDetailsConfiguration : IEntityTypeConfiguration<MovieDetails>
{
    public void Configure(EntityTypeBuilder<MovieDetails> builder)
    {
        builder.HasKey(movie => movie.Id);

        builder.Property(movie => movie.CreatedAt).IsRequired();
        builder.Property(movie => movie.UpdatedAt).IsRequired();
        builder.Property(movie => movie.DeletedAt);
    }
}
