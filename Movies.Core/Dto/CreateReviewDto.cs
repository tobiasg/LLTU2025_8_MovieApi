using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Dto;

public record CreateReviewDto(
    [Required] Guid MovieID,
    [Required][Range(1.0, 5.0)] double Rating,
    [Required] string Name,
    [Required] string Comment
);
