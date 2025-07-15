using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core;

public interface IMovieRepository : IRepositoryBase<Movie>
{
    Task<List<Movie>> GetMoviesAsync(bool trackChanges = false);
    Task<Movie?> GetMovieAsync(Guid id, bool trackChanges = false);
}
