using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Exceptions;

public class ValidationException(string message) : Exception(message)
{
}
