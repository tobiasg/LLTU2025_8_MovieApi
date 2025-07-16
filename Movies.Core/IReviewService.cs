using Movies.Core.Dto;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetReviewsAsync(Guid movieId, bool trackChanges = false);
    Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto createReviewDto);
    Task UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto);
    Task DeleteReviewAsync(Guid id);
}