using Movies.Core.Dto;
using System.Collections;

namespace Movies.Core;

public interface IMovieService
{
    Task<PagedResponse<MovieDto>> GetMoviesAsync(PagingOptions pagingOptions, bool trackChanges = false);
    Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false);
    Task<MovieDetailsDto> GetMovieDetailsAsync(Guid id, bool trackChanges = false);
    Task<MovieDetailsDto> GetTopRatedMovieAsync(bool trackChanges = false);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto);
    Task UpdateMovieAsync(Guid id, UpdateMovieDto updateMovieDto);
    Task DeleteMovieAsync(Guid id);
}