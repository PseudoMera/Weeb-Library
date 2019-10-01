using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeebLibraryApi.Models;

namespace WeebLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeMangaController : ControllerBase
    {
        private readonly WeebLibraryContext _context;

        public AnimeMangaController(WeebLibraryContext context)
        {
            _context = context;
        }

        // GET: api/AnimeManga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeManga>>> GetAnimeMangas()
        {
            return await _context.AnimeMangas.ToListAsync();
        }

        // GET: api/AnimeManga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeManga>> GetAnimeManga(int id)
        {
            var animeManga = await _context.AnimeMangas.FindAsync(id);

            if (animeManga == null)
            {
                return NotFound();
            }

            return animeManga;
        }

        // PUT: api/AnimeManga/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimeManga(int id, AnimeManga animeManga)
        {
            if (id != animeManga.AnimeMangaId)
            {
                return BadRequest();
            }

            _context.Entry(animeManga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeMangaExists(id))
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

        // POST: api/AnimeManga
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnimeManga>> PostAnimeManga(AnimeManga animeManga)
        {
            //If the anime/manga does not exist, we should add it and link it to the useranimemangatable
            //else we should just make a link in the intermediate table
            var myAnimeManga = _context.AnimeMangas.FromSqlInterpolated($"SELECT * FROM AnimeMangas WHERE MalCode = {animeManga.MalCode}");
            if(myAnimeManga == null) 
            {
                _context.AnimeMangas.Add(animeManga);
                await _context.SaveChangesAsync();
            }
            // else
            // {
            //     UserAnimeManga uAnimeManga = new UserAnimeManga();
            //     uAnimeManga.AnimeMangaId = animeManga.AnimeMangaId;
            //    // uAnimeManga.UserId = user.UserId;
            //     _context.UserAnimeMangas.Add(uAnimeManga);

            // }
            return CreatedAtAction("GetAnimeManga", new { id = animeManga.AnimeMangaId }, animeManga);
        }

        // DELETE: api/AnimeManga/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnimeManga>> DeleteAnimeManga(int id)
        {
            var animeManga = await _context.AnimeMangas.FindAsync(id);
            if (animeManga == null)
            {
                return NotFound();
            }

            _context.AnimeMangas.Remove(animeManga);
            await _context.SaveChangesAsync();

            return animeManga;
        }

        private bool AnimeMangaExists(int id)
        {
            return _context.AnimeMangas.Any(e => e.AnimeMangaId == id);
        }
    }
}
