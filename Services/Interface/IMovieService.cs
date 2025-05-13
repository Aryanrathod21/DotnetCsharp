using Repository.Models;
using Repository.ViewModels;

namespace Services.Interface;

public interface IMovieService
{
    Task<Movie?> GetMoviesById(int id);

    Task<PaginatedListViewModel<Movie>> GetAllMovies(string searchString, int page, int pageSize);

    Task<(bool Success, string Message)> AddMovieAsync(CrudMovieViewModel model);

    Task<CrudMovieViewModel?> GetMovieForEdit(int id);

    Task<(bool Success, string Message)> EditMovieAsync(CrudMovieViewModel model);

    Task<(bool Success, string Message)> SoftDeleteMovieAsync(int id);

    Task<List<CrudMovieViewModel>> GetMoviesByPartialNameAsync(string partialName, int limit);

}
