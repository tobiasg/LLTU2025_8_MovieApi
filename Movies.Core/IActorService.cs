using Movies.Core.Dto;

namespace Movies.Core;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetActorsAsync(bool trackChanges = false);
    Task<ActorDetailsDto> GetActorAsync(Guid id, bool trackChanges = false);
    Task<ActorDetailsDto> AddActorToMovie(Guid movieId, Guid actorMovie, bool trackChanges = false);
}