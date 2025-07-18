using Movies.Core.Entities;

namespace Movies.Core;

public interface IReviewRepository : IRepositoryBase<Review>
{
    Task<(List<Review>, int totalItems)> GetReviewsAsync(PagingOptions pagingOptions, Guid movieId, bool trackChanges = false);
    Task<Review?> GetReviewAsync(Guid id, bool trackChanges = false);
}
