using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Services;

public class ActorService(ITransactionManager transactionManager, IMapper mapper) : IActorService
{
    public async Task<IEnumerable<ActorDto>> GetActorsAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<ActorDto>>(await transactionManager.ActorRepository.GetActorsAsync(trackChanges));
    }

    public async Task<ActorDetailsDto> GetActorAsync(Guid id, bool trackChanges = false)
    {
        var actor = await transactionManager.ActorRepository.GetActorAsync(id, trackChanges);
        return actor == null ? null! : mapper.Map<ActorDetailsDto>(actor);
    }

    public async Task<ActorDetailsDto> AddActorToMovie(Guid movieId, Guid actorId, bool trackChanges = false)
    {
        var actor = await transactionManager.ActorRepository.AddActorToMovie(movieId, actorId, trackChanges);
        await transactionManager.CompleteAsync();
        return mapper.Map<ActorDetailsDto>(actor);
    }
}