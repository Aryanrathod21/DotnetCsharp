using Repository.Models;

namespace Repository.Interface;

public interface IMovieRepository
{
    Task<Movie?> GetMovieById(int id);

    Task<List<Movie>> GetAllMovies();

    Task AddMovieAsync(Movie movie);

    Task EditMovieAsync(Movie movie);

    Task<List<Movie>> GetMoviesByPartialNameAsync(string partialName, int limit);
}
