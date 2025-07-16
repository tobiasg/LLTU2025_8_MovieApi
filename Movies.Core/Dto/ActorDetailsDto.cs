using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record ActorDetailsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime? BirhtDate { get; init; }
    public List<MovieDto> Movies { get; init; } = new();
};
