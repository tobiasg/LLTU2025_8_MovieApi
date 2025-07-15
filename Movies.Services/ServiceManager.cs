using Movies.Core;

namespace Movies.Services;

public class ServiceManager(Lazy<IMovieService> movieService) : IServiceManager
{
    public IMovieService MovieService => movieService.Value;
}
