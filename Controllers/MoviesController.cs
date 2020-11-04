using kinopoisk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinopoisk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly KinoContext _context;

        public MoviesController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }


        // GET: api/Movies/name/asc
        [HttpGet("name/asc")]
        public IOrderedQueryable<Movie> GetMoviesNameAsc()
        {
            var sortedMovies = _context.Movies.OrderBy(m => m.Name);
            return sortedMovies;
        }


        // GET: api/Movies/name/desc
        [HttpGet("name/desc")]
        public IEnumerable<Movie> GetMoviesNameDesc()
        {
            var sortedMovies = _context.Movies.OrderByDescending(m => m.Name);
            return sortedMovies;
        }



        // GET: api/Movies/date/asc
        [HttpGet("date/asc")]
        public IOrderedQueryable<Movie> GetMoviesDateAsc()
        {
            var sortedMovies = _context.Movies.OrderBy(m => m.ReleaseDate);
            return sortedMovies;
        }


        // GET: api/Movies/date/desc
        [HttpGet("date/desc")]
        public IEnumerable<Movie> GetMoviesDateDesc()
        {
            var sortedMovies = _context.Movies.OrderByDescending(m => m.ReleaseDate);
            return sortedMovies;
        }

        // GET: api/Movies/rating/asc
        [HttpGet("rating/asc")]
        public IOrderedQueryable<Movie> GetMoviesRatingAsc()
        {
            var sortedMovies = _context.Movies.OrderBy(m => m.Rating);
            return sortedMovies;
        }


        // GET: api/Movies/rating/desc
        [HttpGet("rating/desc")]
        public IEnumerable<Movie> GetMoviesRatingDesc()
        {
            var sortedMovies = _context.Movies.OrderByDescending(m => m.Rating);
            return sortedMovies;
        }



        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.Actors).ThenInclude(t => t.Actor)
                                       .FirstAsync(p => p.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPut("{id}/Actor/{ActorId}")]
        public async Task<IActionResult> AddMovie(int id, int ActorId)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id == id);
            var movie = _context.Movies.FirstOrDefault(m => m.Id == ActorId);

            if (actor == null || movie == null)
            {
                return BadRequest();
            }
            movie.Actors.Add(new MovieActors { ActorId = ActorId, MovieId = id });

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
