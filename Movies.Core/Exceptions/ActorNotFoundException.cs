using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Exceptions;

public class ActorNotFoundException : NotFoundException
{
    public ActorNotFoundException(Guid id) : base($"Actor with id {id} not found") { }
}
