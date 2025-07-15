using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;

namespace Movies.Services;

public class MovieService(ITransactionManager transactionManager, IMapper mapper) : IMovieService
{
    public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<MovieDto>>(await transactionManager.MovieRepository.GetMoviesAsync(trackChanges));
    }

    public async Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id, trackChanges);
        return movie == null ? null! : mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDetailsDto> GetMovieDetailsAsync(Guid id, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovieAsync(UpdateMovieDto updateMovieDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovieAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
