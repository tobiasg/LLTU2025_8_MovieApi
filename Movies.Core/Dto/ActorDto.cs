using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record ActorDto(
    Guid Id,
    string Name,
    DateTime BirthDate
);
