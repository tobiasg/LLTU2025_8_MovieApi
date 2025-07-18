using Movies.Core;
using Movies.Core.Entities;

public interface IGenreRepository : IRepositoryBase<Genre>
{
    Task<(List<Genre>, int totalItems)> GetGenresAsync(PagingOptions pagingOptions, bool trackChanges = false);
    Task<List<Genre>> GetGenresByIds(List<Guid> ids, bool trackChanges = false);
}