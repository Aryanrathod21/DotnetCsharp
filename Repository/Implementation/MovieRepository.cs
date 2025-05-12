using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interface;
using Repository.Models;

namespace Repository.Implementation;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }

    public async Task EditMovieAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetAllMovies()
    {
        return await _context.Movies.ToListAsync();
    }


    public async Task<Movie?> GetMovieById(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }
}
