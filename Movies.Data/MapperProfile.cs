using AutoMapper;
using Movies.Core.Dto;
using Movies.Core.Entities;

namespace Movies.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Movie, MovieDto>().ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => 
            src.Reviews.Count > 0 ? Math.Round(src.Reviews.Average(r => r.Rating), 1) : 0
        ));
        CreateMap<MovieDto, Movie>();

        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                src.Reviews.Count > 0 ? Math.Round(src.Reviews.Average(r => r.Rating), 1) : 0
            ))
            .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Details != null ? src.Details.Synopsis : string.Empty))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Details != null ? src.Details.Language : string.Empty))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Details != null ? src.Details.Budget : 0m));

        CreateMap<MovieDetailsDto, Movie>();

        CreateMap<Genre, GenreDto>();
        CreateMap<GenreDto, Genre>();

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, Review>();
 
        CreateMap<Actor, ActorDto>();
        CreateMap<ActorDto, Actor>();

        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
    }
}
