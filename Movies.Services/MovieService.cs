using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Entities;

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
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id, trackChanges);
        return movie == null ? null! : mapper.Map<MovieDetailsDto>(movie);
    }

    public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
    {
        var movie = mapper.Map<Movie>(createMovieDto);
        
        transactionManager.MovieRepository.Create(movie);

        await transactionManager.CompleteAsync();

        return mapper.Map<MovieDto>(movie);
    }

    public async Task UpdateMovieAsync(Guid id, UpdateMovieDto updateMovieDto)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id, trackChanges: true);
        mapper.Map(updateMovieDto, movie);
        await transactionManager.CompleteAsync();
    }

    public async Task DeleteMovieAsync(Guid id)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id);
        if (movie != null)
        {
            transactionManager.MovieRepository.Delete(movie);
            await transactionManager.CompleteAsync();
        }
    }
}
