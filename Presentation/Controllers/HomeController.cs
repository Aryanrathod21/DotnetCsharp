using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Repository.Models;
using Repository.ViewModels;
using Services.Interface;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IMovieService _movieService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> MovieList(string searchString = " ", int page = 1, int pageSize = 5)
    {

        PaginatedListViewModel<Movie> paginatedMovies = await _movieService.GetAllMovies(searchString, page, pageSize);
        return PartialView("_MovieListPartialView", paginatedMovies);
    }

    [HttpGet]
    public IActionResult AddNewMovie()
    {
        return PartialView("_AddMoviePartialView");
    }

    [HttpGet]
    public async Task<IActionResult> GetMovieSuggestions(string term)
    {
        List<CrudMovieViewModel> movies = await _movieService.GetMoviesByPartialNameAsync(term, 10);
        return Json(movies.Select(m => new
        {
            Id = m.Id,
            Name = m.Name,
            ReleaseYear = m.ReleaseYear,
            ImdbRating = m.ImdbRating,
            BoxOfficeCollection = m.BoxOfficeCollection
        }));
    }

    [HttpGet]
    public async Task<IActionResult> GetMovieById(int id)
    {
        Movie? movie = await _movieService.GetMoviesById(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Json(new
        {
            Id = movie.Id,
            Name = movie.Name,
            ReleaseYear = movie.ReleaseYear,
            ImdbRating = movie.ImdbRating,
            BoxOfficeCollection = movie.BoxOfficeCollection
        });
    }

    [HttpGet]
    public IActionResult Example()
    {
        return PartialView("_ExampleMoviePartialView");
    }

    [HttpPost]
    public async Task<IActionResult> AddNewMovie(CrudMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            string[] errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToArray();
            return Json(new { success = false, message = string.Join(" ", errors) });
        }

        var result = await _movieService.AddMovieAsync(model);
        if (result.Success)
        {
            return Json(new { success = true, message = "Movie Added Successfully" });
        }
        else
        {
            return Json(new { success = false, message = result.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditMovie(int id)
    {
        CrudMovieViewModel? model = await _movieService.GetMovieForEdit(id);
        if (model == null)
        {
            return NotFound("Movie Not Found");
        }
        return PartialView("_EditMoviePartialView", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMovie(CrudMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            string[] errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToArray();
            return Json(new { success = false, message = string.Join(" ", errors) });
        }

        var result = await _movieService.EditMovieAsync(model);
        if (result.Success)
        {
            return Json(new { success = true, message = "Movie Edited Successfully" });
        }
        else
        {
            return Json(new { success = false, message = result.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var result = await _movieService.SoftDeleteMovieAsync(id);
        if (result.Success)
        {
            return Json(new { success = true, message = "Movie Deleted Successfully" });
        }
        else
        {
            return Json(new { success = false, message = result.Message });
        }
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
