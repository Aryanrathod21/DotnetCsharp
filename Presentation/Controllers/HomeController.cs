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

    public async Task<IActionResult> MovieList()
    {
        List<Movie> movies = await _movieService.GetAllMovies();
        return PartialView("_MovieListPartialView", movies);
    }

    [HttpGet]
    public IActionResult AddNewMovie()
    {
        return PartialView("_AddMoviePartialView");
    }

    [HttpPost]
    public async Task<IActionResult> AddNewMovie(CrudMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Collect all validation errors
            var errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToArray();
            return Json(new { success = false, message = string.Join(" ", errors) });
        }

        var result = await _movieService.AddMovieAsync(model);
        if (result.Success)
        {
            // Return JSON indicating success
            return Json(new { success = true, message = "Movie Added Successfully" });
        }
        else
        {
            // Return JSON with the error message from your service
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
