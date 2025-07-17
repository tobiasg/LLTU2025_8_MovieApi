using Movies.Core.Entities;

namespace Movies.Core;

public interface IActorRepository : IRepositoryBase<Actor>
{
    Task<List<Actor>> GetActorsAsync(bool trackChanges = false);
    Task<Actor?> GetActorAsync(Guid id, bool trackChanges = false);
    Task<Actor?> AddActorToMovie(Guid movieId, Guid actorId, bool trackChanges = false);
    Task<List<Actor>> GetMostActiveActorsAsync(bool trackChanges = false);
}
