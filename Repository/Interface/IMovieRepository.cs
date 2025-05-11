using Repository.Models;

namespace Repository.Interface;

public interface IMovieRepository
{
    Task<Movie?> GetMovieById(int id);

    Task<List<Movie>> GetAllMovies();
}
