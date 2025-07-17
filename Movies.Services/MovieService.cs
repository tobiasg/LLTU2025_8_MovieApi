using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Entities;
using Movies.Core.Exceptions;

namespace Movies.Services;

public class MovieService(ITransactionManager transactionManager, IMapper mapper) : IMovieService
{
    public async Task<PagedResponse<MovieDto>> GetMoviesAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var (items, totalItems) = await transactionManager.MovieRepository.GetMoviesAsync(pagingOptions, trackChanges);
        return new PagedResponse<MovieDto>(
            mapper.Map<IEnumerable<MovieDto>>(items),
            new PagingMeta(totalItems, pagingOptions.Page, pagingOptions.Size)
        );
    }

    public async Task<MovieDto> GetMovieAsync(Guid id, bool trackChanges = false)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id, trackChanges);
        return movie == null ? throw new MovieNotFoundException(id) : mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDetailsDto> GetMovieDetailsAsync(Guid id, bool trackChanges = false)
    {
        var movie = await transactionManager.MovieRepository.GetMovieAsync(id, trackChanges);
        return movie == null ? throw new MovieNotFoundException(id) : mapper.Map<MovieDetailsDto>(movie);
    }

    public async Task<MovieDetailsDto> GetTopRatedMovieAsync(bool trackChanges = false)
    {
        var movie = await transactionManager.MovieRepository.GetTopRatedMovieAsync();
        return movie == null ? throw new MovieNotFoundException() : mapper.Map<MovieDetailsDto>(movie);
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
