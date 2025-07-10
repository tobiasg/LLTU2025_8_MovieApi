using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record MovieDetailsDto(
    Guid Id,
    string Title,
    int Year,
    int Duration,
    double AverageRating,
    string Synopsis,
    string Language,
    decimal Budget,
    List<GenreDto> Genres,
    List<ReviewDto> Reviews,
    List<ActorDto> Actors
);