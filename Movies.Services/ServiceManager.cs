using Movies.Core;

namespace Movies.Services;

public class ServiceManager(
    Lazy<IMovieService> movieService, 
    Lazy<IReviewService> reviewService, 
    Lazy<IActorService> actorService,
    Lazy<IGenreService> genreService) : IServiceManager
{
    public IMovieService MovieService => movieService.Value;
    public IReviewService ReviewService => reviewService.Value;
    public IActorService ActorService => actorService.Value;
    public IGenreService GenreService => genreService.Value;
}
