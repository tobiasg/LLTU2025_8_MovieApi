using Movies.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data;

public class TransactionManager(ApplicationContext context, Lazy<IMovieRepository> movieRepository) : ITransactionManager
{
    private readonly Lazy<IMovieRepository> movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    public IMovieRepository MovieRepository => movieRepository.Value;

    private readonly ApplicationContext context = context;

    public Task CompleteAsync() => context.SaveChangesAsync();
}
