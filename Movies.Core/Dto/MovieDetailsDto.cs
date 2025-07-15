using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;
public record MovieDetailsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public int Duration { get; init; }
    public double AverageRating { get; init; }
    public string Synopsis { get; init; } = string.Empty;
    public string Language { get; init; } = string.Empty;
    public decimal Budget { get; init; }
    public List<GenreDto> Genres { get; init; } = new();
    public List<ReviewDto> Reviews { get; init; } = new();
    public List<ActorDto> Actors { get; init; } = new();
}