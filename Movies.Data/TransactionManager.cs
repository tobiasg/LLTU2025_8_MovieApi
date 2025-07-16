using Movies.Core;

namespace Movies.Data;

public class TransactionManager(ApplicationContext context, Lazy<IMovieRepository> movieRepository, Lazy<IReviewRepository> reviewRepository) : ITransactionManager
{
    private readonly Lazy<IMovieRepository> movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    public IMovieRepository MovieRepository => movieRepository.Value;

    private readonly Lazy<IReviewRepository> reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
    public IReviewRepository ReviewRepository => reviewRepository.Value;

    private readonly ApplicationContext context = context;

    public Task CompleteAsync() => context.SaveChangesAsync();
}
