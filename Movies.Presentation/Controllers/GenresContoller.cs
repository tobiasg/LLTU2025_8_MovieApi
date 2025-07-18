using Microsoft.AspNetCore.Mvc;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Presentation.Controllers;

[Route("genres")]
[ApiController]
public class GenresController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<GenreDto>>> GetGenres([FromQuery] PagingOptions pagingOptions) => Ok(await serviceManager.GenreService.GetGenresAsync(pagingOptions));
}
