using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public class ReviewRepository(ApplicationContext context) : BaseRepository<Review>(context), IReviewRepository
{
    public async Task<List<Review>> GetReviewsAsync(Guid movieId, bool trackChanges = false)
    {
        return await FindByCondition(review => review.MovieId == movieId, trackChanges).ToListAsync();
    }

    public async Task<Review?> GetReviewAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(review => review.Id == id, trackChanges).FirstOrDefaultAsync();
    }
}
