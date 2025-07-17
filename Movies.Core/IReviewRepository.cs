using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core;

public interface IReviewRepository : IRepositoryBase<Review>
{
    Task<(List<Review>, int totalItems)> GetReviewsAsync(PagingOptions pagingOptions, Guid movieId, bool trackChanges = false);
    Task<Review?> GetReviewAsync(Guid id, bool trackChanges = false);
}
