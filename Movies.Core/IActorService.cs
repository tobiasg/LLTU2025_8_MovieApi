using Movies.Core.Dto;

namespace Movies.Core;

public interface IActorService
{
    Task<PagedResponse<ActorDto>> GetActorsAsync(PagingOptions pagingOptions, bool trackChanges = false);
    Task<ActorDetailsDto> GetActorAsync(Guid id, bool trackChanges = false);
    Task<ActorDetailsDto> AddActorToMovie(Guid movieId, Guid actorMovie, bool trackChanges = false);
    Task<PagedResponse<ActorDto>> GetMostActiveActorsAsync(PagingOptions pagingOptions, bool trackChanges = false);
}