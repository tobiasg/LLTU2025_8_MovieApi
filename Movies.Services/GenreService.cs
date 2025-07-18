using AutoMapper;
using Movies.Core;
using Movies.Core.Dto;
using Movies.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services;

public class GenreService(ITransactionManager transactionManager, IMapper mapper) : IGenreService
{
    public async Task<PagedResponse<GenreDto>> GetGenresAsync(PagingOptions pagingOptions, bool trackChanges = false)
    {
        var (items, totalItems) = await transactionManager.GenreRepository.GetGenresAsync(pagingOptions, trackChanges);
        return new PagedResponse<GenreDto>(
            mapper.Map<IEnumerable<GenreDto>>(items),
            new PagingMeta(totalItems, pagingOptions.Page, pagingOptions.Size)
        );
    }
}