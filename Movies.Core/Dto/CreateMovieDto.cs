using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record CreateMovieDto(
    string Title,
    int Year,
    int Duration,
    List<Guid> Genres,
    CreateMovieDetailsDto? Details
);

public record CreateMovieDetailsDto(
    string Synopsis,
    string Language,
    decimal Budget
);