using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Entities;

public class ReviewService(ITransactionManager transactionManager, IMapper mapper) : IReviewService
{
    public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(Guid movieId, bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<ReviewDto>>(await transactionManager.ReviewRepository.GetReviewsAsync(movieId, trackChanges));
    }

    public async Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto createReviewDto)
    {
        var review = mapper.Map<Review>(createReviewDto);
        review.MovieId = movieId;

        transactionManager.ReviewRepository.Create(review);

        await transactionManager.CompleteAsync();

        return mapper.Map<ReviewDto>(review);
    }

    public async Task UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto)
    {
        var review = await transactionManager.ReviewRepository.GetReviewAsync(id, trackChanges: true);
        mapper.Map(updateReviewDto, review);
        await transactionManager.CompleteAsync();
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await transactionManager.ReviewRepository.GetReviewAsync(id);
        if (review != null)
        {
            transactionManager.ReviewRepository.Delete(review);
            await transactionManager.CompleteAsync();
        }
    }
}
