using Movies.Core.Dto;

namespace Movies.Core;

public interface IGenreService
{
    Task<PagedResponse<GenreDto>> GetGenresAsync(PagingOptions pagingOptions, bool trackChanges = false);
}