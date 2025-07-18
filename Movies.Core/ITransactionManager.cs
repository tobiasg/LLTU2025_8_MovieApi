namespace Movies.Core;

public interface ITransactionManager
{
    IMovieRepository MovieRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IActorRepository ActorRepository { get; }
    IGenreRepository GenreRepository { get; }

    Task CompleteAsync();
}
