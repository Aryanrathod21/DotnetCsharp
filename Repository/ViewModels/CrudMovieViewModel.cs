using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModels;

public class CrudMovieViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is Required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "ReleaseYear is Required")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "BoxOfficeCollection is Required")]
    public string BoxOfficeCollection { get; set; } = null!;

    [Required(ErrorMessage = "ImdbRating is Required")]
    public decimal ImdbRating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
