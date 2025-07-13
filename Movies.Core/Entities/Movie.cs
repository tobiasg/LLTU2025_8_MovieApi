using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Entities;

public class Movie : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Duration { get; set; }
    public MovieDetails? Details { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Actor> Actors { get; set; }
}
