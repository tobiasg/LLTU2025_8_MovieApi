using Movies.Core.Dto;

namespace Movies.Core;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false);
    Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false);
    Task<MovieDetailsDto> GetMovieDetailsAsync(Guid id, bool trackChanges = false);
    Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto);
    Task UpdateMovieAsync(Guid id, UpdateMovieDto updateMovieDto);
    Task DeleteMovieAsync(Guid id);
}