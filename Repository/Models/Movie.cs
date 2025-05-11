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
}
