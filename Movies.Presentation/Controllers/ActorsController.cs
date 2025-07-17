using Microsoft.AspNetCore.Mvc;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Presentation.Controllers;

[Route("actors")]
[ApiController]
[Produces("application/json")]
public class ActorsController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<ActorDto>>> GetActors([FromQuery] PagingOptions pagingOptions) => Ok(await serviceManager.ActorService.GetActorsAsync(pagingOptions));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActorDetailsDto>> GetActor(Guid id) => Ok(await serviceManager.ActorService.GetActorAsync(id));

    [HttpPost("/movies/{movieId}/actors/{actorId}")]
    public async Task<ActionResult<ActorDetailsDto>> AddActorToMovie(Guid movieId, Guid actorId) => Ok(await serviceManager.ActorService.AddActorToMovie(movieId, actorId));
}
