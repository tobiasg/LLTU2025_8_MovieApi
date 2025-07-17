using Microsoft.AspNetCore.Mvc;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Presentation.Controllers;

[Route("movies")]
[ApiController]
[Produces("application/json")]
public class MoviesController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<MovieDto>>> GetMovies([FromQuery] PagingOptions pagingOptions) => Ok(await serviceManager.MovieService.GetMoviesAsync(pagingOptions));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieDto>> GetMovie(Guid id) => Ok(await serviceManager.MovieService.GetMovieAsync(id));

    [HttpGet("{id:guid}/details")]
    public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(Guid id) => Ok(await serviceManager.MovieService.GetMovieDetailsAsync(id));

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto createMovieDto)
    {
        var movie = await serviceManager.MovieService.CreateMovieAsync(createMovieDto);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MovieDto>> UpdateMovie(Guid id, UpdateMovieDto updateMovieDto)
    {
        if (id != updateMovieDto.Id) return BadRequest();
        await serviceManager.MovieService.UpdateMovieAsync(id, updateMovieDto);
        return NoContent(); 
    }

    [HttpDelete("/movies/{id}")]
    public async Task<ActionResult> DeleteMovie(Guid id)
    {
        await serviceManager.MovieService.DeleteMovieAsync(id);
        return NoContent();
    }
}
