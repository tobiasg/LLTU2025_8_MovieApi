using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Presentation.Controllers;

[Route("reviews")]
[ApiController]
[Produces("application/json")]
public class ReviewsController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet("/movies/{movieId:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(Guid movieId) => Ok(await serviceManager.ReviewService.GetReviewsAsync(movieId));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewDto>> GetReview(Guid id) => Ok(await serviceManager.MovieService.GetMovieAsync(id));

    [HttpPost("/movies/{movieId:guid}/reviews")]
    public async Task<ActionResult<ReviewDto>> CreateMovie(Guid movieId, CreateReviewDto createReviewDto)
    {
        var movie = await serviceManager.ReviewService.CreateReviewAsync(movieId, createReviewDto);
        return CreatedAtAction(nameof(GetReview), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReviewDto>> UpdateReview(Guid id, UpdateReviewDto updateReviewDto)
    {
        if (id != updateReviewDto.Id) return BadRequest();
        await serviceManager.ReviewService.UpdateReviewAsync(id, updateReviewDto);
        return NoContent();
    }

    [HttpDelete("/reviews/{id}")]
    public async Task<ActionResult> DeleteReview(Guid id)
    {
        await serviceManager.ReviewService.DeleteReviewAsync(id);
        return NoContent();
    }
}
