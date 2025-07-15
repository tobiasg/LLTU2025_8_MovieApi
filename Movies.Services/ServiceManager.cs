using Movies.Core;

namespace Movies.Services;

public class ServiceManager(Lazy<IMovieService> movieService) : ISeviceManager
{
    public IMovieService MovieService => movieService.Value;
}
