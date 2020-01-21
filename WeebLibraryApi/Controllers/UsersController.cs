using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeebLibraryApi.Models;

namespace WeebLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WeebLibraryContext _context;

        public UserController(WeebLibraryContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [EnableCors("AllowMyOrigin")]
        [HttpPost("login")]
        public ActionResult<User> GetUser(User user)
        {
            var user2 = _context.Users.Where(s => s.Email == user.Email).First();
            if (user2 == null)
            {
                return NotFound();
            }
            else if(user2.Password != user.Password)
            {
                return NotFound();
            }

            return user2;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if(string.IsNullOrEmpty(user.Password) || 
               string.IsNullOrEmpty(user.Username) || 
               string.IsNullOrEmpty(user.Email))
            {
                return BadRequest();
            }
            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }
        [EnableCors("AllowMyOrigin")]
        [HttpPost("animes")]
        public async Task<ActionResult<List<AnimeManga>>> GetUserAnimes(User user)
        {
            var user2 = _context.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Email = {user.Email}").First();
            var animeManga = await _context.AnimeMangas.FromSqlInterpolated(
                $"SELECT AnimeMangas.AnimeMangaId,ImageURL, Title, [Type], MalCode, UserId FROM AnimeMangas INNER JOIN UserAnimeMangas ON AnimeMangas.AnimeMangaId = UserAnimeMangas.AnimeMangaId WHERE UserAnimeMangas.UserId = {user2.UserId} AND [Type] = 'Anime'").ToListAsync();
            return animeManga;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost("mangas")]
        public async Task<ActionResult<List<AnimeManga>>> GetUserMangas(User user)
        {
            var user2 = _context.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Email = {user.Email}").First();
            var animeManga = await _context.AnimeMangas.FromSqlInterpolated(
                $"SELECT AnimeMangas.AnimeMangaId,ImageURL, Title, [Type], MalCode, UserId FROM AnimeMangas INNER JOIN UserAnimeMangas ON AnimeMangas.AnimeMangaId = UserAnimeMangas.AnimeMangaId WHERE UserAnimeMangas.UserId = {user2.UserId} AND [Type] = 'Manga'").ToListAsync();
            return animeManga;
        }
        [EnableCors("AllowMyOrigin")]
        [HttpGet("animesandmangas")]
        public async Task<ActionResult<List<AnimeManga>>> GetUserAnimesAndMangas(User user)
        {
            var user2 = _context.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Email = {user.Email}").First();
            var animeManga = await _context.AnimeMangas.FromSqlInterpolated(
                $"SELECT AnimeMangas.AnimeMangaId,ImageURL, Title, [Type], MalCode, UserId FROM AnimeMangas INNER JOIN UserAnimeMangas ON AnimeMangas.AnimeMangaId = UserAnimeMangas.AnimeMangaId WHERE UserAnimeMangas.UserId = {user2.UserId}").ToListAsync();
            return animeManga;
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
