using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string BoxOfficeCollection { get; set; } = null!;

    public decimal ImdbRating { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
