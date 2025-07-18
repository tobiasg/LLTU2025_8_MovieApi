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
        await ValidateCreateAsync(createMovieDto);

        var movie = mapper.Map<Movie>(createMovieDto);

        if (createMovieDto.Details != null)
        {
            movie.Details = mapper.Map<MovieDetails>(createMovieDto.Details);
        }

        if (createMovieDto.Genres?.Any() == true)
        {
            movie.Genres = await transactionManager.GenreRepository.GetGenresByIds(createMovieDto.Genres, trackChanges: true);
        }

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
    private async Task ValidateCreateAsync(CreateMovieDto createMovieDto)
    {
        var movie = await transactionManager.MovieRepository.GetMovieByTitleAsync(createMovieDto.Title, trackChanges: false);
        if (movie != null) throw new ValidationException("Movie alredy exists");

        if (createMovieDto.Duration < 0) throw new ValidationException("Duration can not be negative");

        if (createMovieDto.Details != null && createMovieDto.Details.Budget < 0) throw new ValidationException("Budget can not be negative");

        if (createMovieDto.Genres?.Any() == true)
        {
            var genres = await transactionManager.GenreRepository.GetGenresByIds(createMovieDto.Genres, trackChanges: false);
            if (genres.Any(g => g.Name.Equals("Documentary", StringComparison.OrdinalIgnoreCase)))
            {
                if (createMovieDto.Details?.Budget > 1000000) throw new ValidationException("Documentary movies cannot have a budget over 1 000 000");
            }
        }
    }
}
