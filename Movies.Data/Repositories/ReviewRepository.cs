using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public class ReviewRepository(ApplicationContext context) : BaseRepository<Review>(context), IReviewRepository
{
    public async Task<(List<Review>, int totalItems)> GetReviewsAsync(PagingOptions pagingOptions, Guid movieId, bool trackChanges = false)
    {
        var query = FindByCondition(review => review.MovieId == movieId, trackChanges);

        return (
            await query.Skip((pagingOptions.Page - 1) * pagingOptions.Size).Take(pagingOptions.Size).ToListAsync(),
            await query.CountAsync()
        );
    }

    public async Task<Review?> GetReviewAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(review => review.Id == id, trackChanges).FirstOrDefaultAsync();
    }
}
