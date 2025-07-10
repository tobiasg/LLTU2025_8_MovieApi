using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class MovieDetails : EntityBase
{
    [Required]
    public string Synopsis { get; set; } = string.Empty;

    [Required]
    public string Language { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}
