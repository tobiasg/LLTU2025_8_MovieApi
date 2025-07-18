using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Entities;
using Movies.Core.Exceptions;

namespace Movies.Services;

public class ReviewService(ITransactionManager transactionManager, IMapper mapper) : IReviewService
{
    public async Task<PagedResponse<ReviewDto>> GetReviewsAsync(PagingOptions pagingOptions, Guid movieId, bool trackChanges = false)
    {
        var (items, totalItems) = await transactionManager.ReviewRepository.GetReviewsAsync(pagingOptions, movieId, trackChanges);
        return new PagedResponse<ReviewDto>(
            mapper.Map<IEnumerable<ReviewDto>>(items),
            new PagingMeta(totalItems, pagingOptions.Page, pagingOptions.Size)
        );
    }

    public async Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto createReviewDto)
    {
        await ValidateCreateAsync(movieId, createReviewDto);

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

    private async Task ValidateCreateAsync(Guid movieId, CreateReviewDto createReviewDto)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(movieId, trackChanges: false);
        if (movie == null) throw new MovieNotFoundException(movieId);

        var reviewsCount = movie.Reviews?.Count ?? 0;
        var movieAge = DateTime.Now.Year - movie.Year;

        if (reviewsCount >= 10) throw new ValidationException("Movie cannot have more than 10 reviews");
        if (movieAge >= 20 && reviewsCount >= 5) throw new ValidationException("Movies older than 20 years cannot have more than 5 reviews");
    }
}
