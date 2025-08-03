using AutoMapper;
using Moq;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Entities;
using Movies.Core.Exceptions;
using Movies.Services;

namespace Movies.Tests;

public class MovieServiceTests
{
    private readonly Mock<ITransactionManager> transactionManager = new();
    private readonly Mock<IMovieRepository> repository = new();
    private readonly Mock<IMapper> mapper = new();
    private readonly MovieService service;

    public MovieServiceTests()
    {
        transactionManager.SetupGet(t => t.MovieRepository).Returns(repository.Object);
        service = new MovieService(transactionManager.Object, mapper.Object);
    }

    [Theory]
    [InlineData(1, 3, 3)]
    [InlineData(2, 3, 3)]
    [InlineData(3, 3, 3)]
    [InlineData(4, 3, 1)]
    [InlineData(5, 3, 0)]
    public async Task GetMoviesAsync_ReturnsPagedResponse_WithMovies(int page, int pageSize, int expectedCount)
    {
        var options = new PagingOptions { Page = page, Size = pageSize };
        var movies = Enumerable.Range(1, 10).Select(i => new Movie { Id = Guid.NewGuid(), Title = $"Movie {i}" }).ToList();
        var pagedMovies = movies.Skip((options.Page - 1) * options.Size).Take(options.Size).ToList();
        var mappedMovies = pagedMovies.Select(movie => new MovieDto { Id = movie.Id, Title = movie.Title }).ToList();

        repository.Setup(repository => repository.GetMoviesAsync(options, false)).ReturnsAsync((pagedMovies, movies.Count));
        mapper.Setup(mapper => mapper.Map<IEnumerable<MovieDto>>(pagedMovies)).Returns(mappedMovies);
        var result = await service.GetMoviesAsync(options);

        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Data.Count());
        Assert.Equal(page, result.Meta.CurrentPage);
        Assert.Equal(movies.Count, result.Meta.TotalItems);
    }

    [Fact]
    public async Task GetMoviesAsync_ReturnsEmptyPagedResponse_WithNoMovies()
    {
        var options = new PagingOptions { Page = 1, Size = 3 };
        var movies = new List<Movie>();
        var mappedMovies = new List<MovieDto>();
        var pagedMovies = movies.Skip((options.Page - 1) * options.Size).Take(options.Size).ToList();

        repository.Setup(repository => repository.GetMoviesAsync(options, false)).ReturnsAsync((pagedMovies, movies.Count));
        mapper.Setup(mapper => mapper.Map<IEnumerable<MovieDto>>(pagedMovies)).Returns(mappedMovies);
        var result = await service.GetMoviesAsync(options);

        Assert.NotNull(result);
        Assert.Empty(result.Data);
        Assert.Equal(0, result.Meta.TotalItems);
        Assert.Equal(1, result.Meta.CurrentPage);
    }

    [Fact]
    public async Task GetMovieAsync_ReturnsMovieDto_WhenMovieExists()
    {
        var movieId = Guid.NewGuid();
        var movie = new Movie { Id = movieId, Title = "Movie 1" };
        var movieDto = new MovieDto { Id = movieId, Title = "Movie 1" };

        repository.Setup(repository => repository.GetMovieAsync(movieId, false)).ReturnsAsync(movie);
        mapper.Setup(mapper => mapper.Map<MovieDto>(movie)).Returns(movieDto);

        var result = await service.GetMovieAsync(movieId);

        Assert.IsType<MovieDto>(result);
        Assert.NotNull(result);
        Assert.Equal("Movie 1", result.Title);
    }

    [Fact]
    public async Task GetMovieAsync_ThrowsException_WhenMovieDoesNotExists()
    {
        var movieId = Guid.NewGuid();

        repository.Setup(repository => repository.GetMovieAsync(movieId, false)).ReturnsAsync((Movie?)null);

        var exception = await Assert.ThrowsAsync<MovieNotFoundException>(async () => await service.GetMovieAsync(movieId));
        Assert.Equal($"Movie with id {movieId} not found", exception.Message);
    }
}
