using Microsoft.AspNetCore.Mvc;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Presentation.Controllers;

[Route("reports")]
[ApiController]
public class ReportsController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet("movies/top-rated-movie")]
    public async Task<ActionResult<MovieDetailsDto>> GetTopRatedMovie() => Ok(await serviceManager.MovieService.GetTopRatedMovieAsync());

    [HttpGet("actors/most-active")]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetMostActiveActors() => Ok(await serviceManager.ActorService.GetMostActiveActorsAsync());
}