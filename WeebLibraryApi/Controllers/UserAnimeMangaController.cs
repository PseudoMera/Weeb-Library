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
    public class UserAnimeMangaController : ControllerBase
    {
        private readonly WeebLibraryContext _context;

        public UserAnimeMangaController(WeebLibraryContext context)
        {
            _context = context;
        }

        // GET: api/UserAnimeManga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAnimeManga>>> GetUserAnimeMangas()
        {
            return await _context.UserAnimeMangas.ToListAsync();
        }

        // GET: api/UserAnimeManga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAnimeManga>> GetUserAnimeManga(int id)
        {
            var userAnimeManga = await _context.UserAnimeMangas.FindAsync(id);

            if (userAnimeManga == null)
            {
                return NotFound();
            }

            return userAnimeManga;
        }

        // PUT: api/UserAnimeManga/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAnimeManga(int id, UserAnimeManga userAnimeManga)
        {
            if (id != userAnimeManga.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userAnimeManga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAnimeMangaExists(id))
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

        // POST: api/UserAnimeManga
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserAnimeManga>> PostAnimeManga(Helper helper)
        {
            //If the anime/manga does not exist, we should add it and link it to the useranimemangatable
            //else we should just make a link in the intermediate table
            var myAnimeManga =  new AnimeManga();
            var user = new User();
            try {
               myAnimeManga =  _context.AnimeMangas.FromSqlInterpolated($"SELECT * FROM AnimeMangas WHERE MalCode = {helper.AnimeManga.MalCode}").First();
            } 
            catch(System.Exception e) 
            {
                _context.AnimeMangas.Add(helper.AnimeManga);
                await _context.SaveChangesAsync();
                myAnimeManga =  _context.AnimeMangas.FromSqlInterpolated($"SELECT * FROM AnimeMangas WHERE MalCode = {helper.AnimeManga.MalCode}").First();
            }

            try {
                user = _context.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Email = {helper.Email}").First();

            }
            catch(System.Exception e)
            {
                return NotFound();
            }

            UserAnimeManga userAnimeManga= new UserAnimeManga();
            userAnimeManga.AnimeMangaId = myAnimeManga.AnimeMangaId;
            userAnimeManga.UserId = user.UserId;
            _context.UserAnimeMangas.Add(userAnimeManga);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetUserAnimeManga", new { id = userAnimeManga.AnimeMangaId }, userAnimeManga);
        }
        
        // [HttpPost]
        // public async Task<ActionResult<UserAnimeManga>> PostUserAnimeManga(UserAnimeManga userAnimeManga)
        // {
        //     _context.UserAnimeMangas.Add(userAnimeManga);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (UserAnimeMangaExists(userAnimeManga.UserId))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetUserAnimeManga", new { id = userAnimeManga.UserId }, userAnimeManga);
        // }

        // DELETE: api/UserAnimeManga/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAnimeManga>> DeleteUserAnimeManga(int id)
        {
            var userAnimeManga = await _context.UserAnimeMangas.FindAsync(id);
            if (userAnimeManga == null)
            {
                return NotFound();
            }

            _context.UserAnimeMangas.Remove(userAnimeManga);
            await _context.SaveChangesAsync();

            return userAnimeManga;
        }

        private bool UserAnimeMangaExists(int id)
        {
            return _context.UserAnimeMangas.Any(e => e.UserId == id);
        }
    }
}
