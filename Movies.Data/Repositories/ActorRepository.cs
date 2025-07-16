using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class ActorRepository(ApplicationContext context) : BaseRepository<Actor>(context), IActorRepository
{
    public async Task<List<Actor>> GetActorsAsync(bool trackChanges = false)
    {
        return await FindAll(trackChanges).ToListAsync();
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
        var movie = await context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null) throw new ArgumentException($"Movie not found");

        var actor = await FindByCondition(actor => actor.Id == actorId, trackChanges)
            .Include(a => a.Movies).ThenInclude(m => m.Genres)
            .Include(a => a.Movies).ThenInclude(m => m.Reviews)
            .FirstOrDefaultAsync();

        if (actor == null) throw new ArgumentException($"Actor not found");

        if (movie.Actors.Any(a => a.Id == actorId))
        { 
            movie.Actors.Add(actor);
        }

        return actor;
    }
}
