using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;
using System.Linq.Expressions;

namespace Movies.Data.Repositories;

public class GenreRepository(ApplicationContext context) : BaseRepository<Genre>(context), IGenreRepository
{
    public async Task<(List<Genre>, int totalItems)> GetGenresAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var query = FindAll(trackChanges);

        return (
            await query.Skip((pagingOptions.Page - 1) * pagingOptions.Size).Take(pagingOptions.Size).ToListAsync(),
            await query.CountAsync()
        );
    }

    public async Task<List<Genre>> GetGenresByIds(List<Guid> ids, bool trackChanges = false)
    {
        return await FindByCondition(genre => ids.Contains(genre.Id), trackChanges).ToListAsync();
    }
}
