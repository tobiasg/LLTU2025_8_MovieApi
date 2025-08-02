namespace Movies.Core.Dto;

public class UpdateMovieDetailsDto
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public int? Duration { get; set; }
    public string? Synopsis { get; set; }
    public string? Language { get; set; }
    public decimal? Budget { get; set; }
}