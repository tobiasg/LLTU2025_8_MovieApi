namespace Movies.Core;

public interface ITransactionManager
{
    IMovieRepository MovieRepository { get; }

    Task CompleteAsync();
}
