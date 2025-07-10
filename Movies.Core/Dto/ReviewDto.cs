using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record ReviewDto(
    Guid Id,
    double Rating,
    string Name,
    string Comment
);
