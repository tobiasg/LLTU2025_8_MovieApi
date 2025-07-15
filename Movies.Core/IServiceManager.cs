namespace Movies.Core;

public interface IServiceManager
{
    IMovieService MovieService { get; }
}