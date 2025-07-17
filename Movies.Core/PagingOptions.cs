using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core;

public class PagingOptions
{
    private readonly int MaxSize = 100;

    private int _page = 1;
    private int _size = 10;

    public int Page
    {
        get => _page;
        set => _page = Math.Max(1, value);
    }

    public int Size
    {
        get => _size;
        set => _size = Math.Max(1, Math.Min(value, MaxSize));
    }
}