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

    public async Task<PaginatedListViewModel<Movie>> GetAllMovies(string searchString, int page, int pageSize)
    {
        // return await _movieRepository.GetAllMovies();
        var movies = await _movieRepository.GetAllMovies();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            movies = movies.Where(m => m.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }

        int totalItems = movies.Count;
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var paginatedTables = movies
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedListViewModel<Movie>
        {
            Items = paginatedTables,
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = page,
            TotalPages = totalPages,
            SearchString = searchString
        };
    }

    public async Task<CrudMovieViewModel?> GetMovieForEdit(int id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null)
        {
            return null;
        }
        return new CrudMovieViewModel
        {
            Name = movie.Name,
            ReleaseYear = movie.ReleaseYear,
            BoxOfficeCollection = movie.BoxOfficeCollection,
            ImdbRating = movie.ImdbRating
        };
    }


    public async Task<(bool Success, string Message)> EditMovieAsync(CrudMovieViewModel model)
    {
        var originalname = model.Name?.Trim();
        if (string.IsNullOrEmpty(originalname))
        {
            return (false, "Movie name cannot be empty.");
        }

        var normalizedname = originalname.Trim().ToLowerInvariant();
        var existingmovie = await _movieRepository.GetAllMovies();
        if (existingmovie.Any(m => m.Name?.Trim().ToLowerInvariant() == normalizedname && m.Id != model.Id))
        {
            return (false, "Movie Already Exists.");
        }

        var movie = await _movieRepository.GetMovieById(model.Id);
        if (movie == null)
        {
            return (false, "Movie Not Found");
        }

        movie.Name = model.Name;
        movie.ReleaseYear = model.ReleaseYear;
        movie.ImdbRating = model.ImdbRating;
        movie.BoxOfficeCollection = model.BoxOfficeCollection;
        movie.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _movieRepository.EditMovieAsync(movie);
            return (true, "Movie Edited SuccessFully");
        }
        catch (Exception ex)
        {
            return (false, "Failed to Edit Movie");
        }
    }


    public async Task<(bool Success, string Message)> SoftDeleteMovieAsync(int id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null)
        {
            return (false, "Movie Not Found");
        }

        movie.IsDeleted = true;
        await _movieRepository.EditMovieAsync(movie);
        return (true, "Movie Deleted Successfully");
    }

    public async Task<Movie?> GetMoviesById(int id)
    {
        return await _movieRepository.GetMovieById(id);
    }

    public async Task<List<CrudMovieViewModel>> GetMoviesByPartialNameAsync(string partialName, int limit)
    {
        var movies = await _movieRepository.GetMoviesByPartialNameAsync(partialName, limit);
        return movies.Select(m => new CrudMovieViewModel
        {
            Id = m.Id, // Add Id
            Name = m.Name,
            ReleaseYear = m.ReleaseYear,
            ImdbRating = m.ImdbRating,
            BoxOfficeCollection = m.BoxOfficeCollection
        }).ToList();
    }

}
