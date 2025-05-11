using Repository.Interface;
using Repository.Models;
using Services.Interface;

namespace Services.Implementation;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<List<Movie>> GetAllMovies()
    {
        return await _movieRepository.GetAllMovies();
    }


    public async Task<Movie?> GetMoviesById(int id)
    {
        return await _movieRepository.GetMovieById(id);
    }

}
