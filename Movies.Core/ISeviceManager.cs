namespace Movies.Core;

public interface ISeviceManager
{
    IMovieService MovieService { get; }
}