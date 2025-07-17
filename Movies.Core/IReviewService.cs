using Movies.Core.Dto;

namespace Movies.Core;

public interface IReviewService
{
    Task<PagedResponse<ReviewDto>> GetReviewsAsync(PagingOptions pagingOptions, Guid movieId, bool trackChanges = false);
    Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto createReviewDto);
    Task UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto);
    Task DeleteReviewAsync(Guid id);
}