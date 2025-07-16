namespace Movies.Core;

public interface ITransactionManager
{
    IMovieRepository MovieRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IActorRepository ActorRepository { get; }

    Task CompleteAsync();
}
