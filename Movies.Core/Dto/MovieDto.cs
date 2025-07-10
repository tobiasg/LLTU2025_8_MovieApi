using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record MovieDto(
    Guid Id,
    string Title,
    int Year,
    int Duration,
    double AverageRating,
    List<GenreDto> Genres
);
