using Repository.Interface;
using Repository.Models;
using Repository.ViewModels;
using Services.Interface;

namespace Services.Implementation;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<(bool Success, string Message)> AddMovieAsync(CrudMovieViewModel model)
    {
        var originalname = model.Name?.Trim();
        if (string.IsNullOrEmpty(originalname))
        {
            return (false, "Movie name cannot be empty.");
        }

        var normalizedname = originalname.ToLowerInvariant();

        var existingmovie = await _movieRepository.GetAllMovies();
        if (existingmovie.Any(m => m.Name?.ToLowerInvariant() == normalizedname))
        {
            return (false, "Movie Already Exists.");
        }

        var movie = new Movie
        {
            Name = model.Name,
            ReleaseYear = model.ReleaseYear,
            BoxOfficeCollection = model.BoxOfficeCollection,
            ImdbRating = model.ImdbRating,
            CreatedAt = DateTime.UtcNow
        };
        try
        {
            await _movieRepository.AddMovieAsync(movie);
            return (true, "Movie Added SuccessFully");
        }
        catch (Exception ex)
        {
            return (false, "Failed to Add Movie");
        }
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
