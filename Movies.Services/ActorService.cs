using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Services;

public class ActorService(ITransactionManager transactionManager, IMapper mapper) : IActorService
{
    public async Task<PagedResponse<ActorDto>> GetActorsAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var (items, totalItems) = await transactionManager.ActorRepository.GetActorsAsync(pagingOptions, trackChanges);
        return new PagedResponse<ActorDto>(
            mapper.Map<IEnumerable<ActorDto>>(items),
            new PagingMeta(totalItems, pagingOptions.Page, pagingOptions.Size)
        );
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

    public async Task<PagedResponse<ActorDto>> GetMostActiveActorsAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var (items, totalItems) = await transactionManager.ActorRepository.GetMostActiveActorsAsync(pagingOptions, trackChanges);
        return new PagedResponse<ActorDto>(
            mapper.Map<IEnumerable<ActorDto>>(items),
            new PagingMeta(totalItems, pagingOptions.Page, pagingOptions.Size)
        );

        //return mapper.Map<IEnumerable<ActorDto>>();
    }
}