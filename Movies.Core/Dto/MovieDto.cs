using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record MovieDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public int Duration { get; init; }
    public double AverageRating { get; init; }
    public List<GenreDto> Genres { get; init; } = new();
}