using Movies.Core;

namespace Movies.Data;

public class TransactionManager(
    ApplicationContext context, 
    Lazy<IMovieRepository> movieRepository, 
    Lazy<IReviewRepository> reviewRepository,
    Lazy<IActorRepository> actorRepository) : ITransactionManager
{
    private readonly Lazy<IMovieRepository> movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    public IMovieRepository MovieRepository => movieRepository.Value;

    private readonly Lazy<IReviewRepository> reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
    public IReviewRepository ReviewRepository => reviewRepository.Value;

    private readonly Lazy<IActorRepository> actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
    public IActorRepository ActorRepository => actorRepository.Value;

    private readonly ApplicationContext context = context;

    public Task CompleteAsync() => context.SaveChangesAsync();
}
