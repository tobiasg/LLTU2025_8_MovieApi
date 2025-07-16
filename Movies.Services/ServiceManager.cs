using Movies.Core;

namespace Movies.Services;

public class ServiceManager(Lazy<IMovieService> movieService, Lazy<IReviewService> reviewService) : IServiceManager
{
    public IMovieService MovieService => movieService.Value;
    public IReviewService ReviewService => reviewService.Value;
}
