using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class MovieRepository(ApplicationContext context) : BaseRepository<Movie>(context), IMovieRepository
{
    public async Task<(List<Movie>, int totalItems)> GetMoviesAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var query = FindAll(trackChanges).Include(movie => movie.Genres).Include(movie => movie.Reviews);

        return (
            await query.Skip((pagingOptions.Page -1) * pagingOptions.Size).Take(pagingOptions.Size).ToListAsync(), 
            await query.CountAsync()
        );
    }

    public async Task<Movie?> GetMovieAsync(Guid id, bool trackChanges = false)
    {
        return await FindByCondition(movie => movie.Id == id, trackChanges)
            .Include(movie => movie.Details)
            .Include(movie => movie.Genres)
            .Include(movie => movie.Reviews)
            .Include(movie => movie.Actors)
            .FirstOrDefaultAsync();
    }

    public async Task<Movie?> GetTopRatedMovieAsync(bool trackChanges = false)
    {
        return await FindByCondition(movie => movie.Reviews.Any(), trackChanges)
             .Include(movie => movie.Details)
            .Include(movie => movie.Genres)
            .Include(movie => movie.Reviews)
            .Include(movie => movie.Actors)
            .OrderByDescending(movie => movie.Reviews.Select(review => review.Rating).Average())
            .FirstOrDefaultAsync();
    }
}