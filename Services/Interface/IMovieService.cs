using Repository.Models;
using Repository.ViewModels;

namespace Services.Interface;

public interface IMovieService
{
    Task<Movie?> GetMoviesById(int id);

    Task<List<Movie>> GetAllMovies();

    Task<(bool Success, string Message)> AddMovieAsync(CrudMovieViewModel model);
}
