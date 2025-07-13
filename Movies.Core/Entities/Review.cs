using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class Review : EntityBase
{
    public double Rating { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}