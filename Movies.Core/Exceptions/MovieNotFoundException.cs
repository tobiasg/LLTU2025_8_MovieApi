using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Exceptions;

public class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(Guid? id = null) : base(id == null ? $"Movie not found" : $"Movie with id {id} not found") { }
}
