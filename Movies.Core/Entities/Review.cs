using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class Review : EntityBase
{
    [Range(1.0, 5.0)]
    public double Rating { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Comment { get; set; } = string.Empty;

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}