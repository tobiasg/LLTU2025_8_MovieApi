namespace Movies.Core;

public interface ITransactionManager
{
    IMovieRepository MovieRepository { get; }
    IReviewRepository ReviewRepository { get; }

    Task CompleteAsync();
}
