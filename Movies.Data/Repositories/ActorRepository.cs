using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;
using Movies.Core.Exceptions;

namespace Movies.Data.Repositories;

public class ActorRepository(ApplicationContext context) : BaseRepository<Actor>(context), IActorRepository
{
    public async Task<(List<Actor>, int totalItems)> GetActorsAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var query = FindAll(trackChanges);

        return (
            await query.Skip((pagingOptions.Page - 1) * pagingOptions.Size).Take(pagingOptions.Size).ToListAsync(),
            await query.CountAsync()
        );
    }

    public async Task<Actor?> GetActorAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(actor => actor.Id == id, trackChanges)
            .Include(a => a.Movies).ThenInclude(m => m.Genres)
            .Include(a => a.Movies).ThenInclude(m => m.Reviews)
            .FirstOrDefaultAsync();
    }

    public async Task<Actor> AddActorToMovie(Guid movieId, Guid actorId, bool trackChanges = false)
    {
        var movie = await context.Movies
            .Include(m => m.Actors)
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == movieId) ?? throw new ValidationException("Movie not found");

        if (!await context.Actors.AnyAsync(a => a.Id == actorId)) throw new ValidationException("Actor not found");

        var actorsCount = movie.Actors.Count;
        var actorsLimitForDocumentaries = 10;

        if (movie.Genres.Any(g => g.Name.Equals("Documentary", StringComparison.OrdinalIgnoreCase)))
        {
            if (actorsCount >= actorsLimitForDocumentaries) throw new ValidationException($"Documentary movies cannot have more than {actorsLimitForDocumentaries} actors");
        }

        var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == actorId) ?? throw new ValidationException("Actor not found");

        if (!movie.Actors.Any(a => a.Id == actorId))
        {
            movie.Actors.Add(actor);
        }

        return actor;
    }

    public async Task<(List<Actor>, int totalItems)> GetMostActiveActorsAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var query = context.Movies
            .SelectMany(movie => movie.Actors)
            .GroupBy(actor => actor.Id)
            .OrderByDescending(group => group.Count())
            .Select(group => group.First());

        return (
            await query.Skip((pagingOptions.Page - 1) * pagingOptions.Size).Take(pagingOptions.Size).ToListAsync(),
            await query.CountAsync()
        );
    }
}
