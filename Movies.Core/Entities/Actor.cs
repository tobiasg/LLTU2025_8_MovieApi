namespace Movies.Core.Entities;

public class Actor : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<Movie> Movies { get; set; }
}
