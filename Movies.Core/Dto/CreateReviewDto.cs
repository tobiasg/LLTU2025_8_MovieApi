using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Dto;

public record CreateReviewDto(
    [Required] [Range(1.0, 5.0)] double Rating,
    [Required] string Name,
    [Required] string Comment
);
