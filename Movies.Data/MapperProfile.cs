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

        CreateMap<Actor, ActorDetailsDto>();
        CreateMap<ActorDetailsDto, Actor>();

        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Details, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Actors, opt => opt.Ignore());

        CreateMap<CreateMovieDetailsDto, MovieDetails>();

        CreateMap<UpdateMovieDto, Movie>();

        CreateMap<CreateReviewDto, Review>();
        CreateMap<UpdateReviewDto, Review>();
    }
}
