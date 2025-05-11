using Repository.Models;

namespace Services.Interface;

public interface IMovieService
{
    Task<Movie?> GetMoviesById(int id);

    Task<List<Movie>> GetAllMovies();
}
