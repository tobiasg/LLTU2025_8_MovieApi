using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core;

public class PagedResponse<T>(IEnumerable<T> data, PagingMeta meta)
{
    public IEnumerable<T> Data { get; set; } = data;
    public PagingMeta Meta { get; set; } = meta;
}
