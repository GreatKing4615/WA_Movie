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
    public class ActorsController : ControllerBase
    {
        private readonly KinoContext _context;

        public ActorsController(KinoContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return await _context.Actors.ToListAsync();
        }

        // GET: api/Actors/rating/asc
        [HttpGet("rating/asc")]
        public IOrderedQueryable<Actor> GetMoviesRatingAsc()
        {
            var sortedActors = _context.Actors.OrderBy(m => m.Rating);
            return sortedActors;
        }


        // GET: api/Actors/rating/desc
        [HttpGet("rating/desc")]
        public IEnumerable<Actor> GetMoviesRatingDesc()
        {
            var sortedActors = _context.Actors.OrderByDescending(m => m.Rating);
            return sortedActors;
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _context.Actors.AsNoTracking()
                                       .AsQueryable()
                                       .Include(c => c.Movies).ThenInclude(t => t.Movie)
                                       .FirstAsync(p => p.Id == id); 

            if (actor == null)
            {
                return NotFound();
            }

            return  actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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


        [HttpPut("{id}/Movie/{MovieId}")]
        public async Task<IActionResult> AddMovie(int id, int MovieId)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id==id);
            var movie = _context.Movies.FirstOrDefault(m => m.Id==MovieId);

            if ( actor==null || movie==null)
            {
                return BadRequest();
            }
            actor.Movies.Add(new MovieActors { ActorId=id,MovieId=MovieId});
            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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


        // POST: api/Actors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Actor>> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return actor;
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
