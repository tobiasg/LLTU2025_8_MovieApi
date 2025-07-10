using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class Genre : EntityBase
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = [];
}