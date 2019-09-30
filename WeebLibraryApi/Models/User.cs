using System.Collections.Generic;

namespace WeebLibraryApi.Models
{
    public class User
    {
        public User()
        {
            UserAnimeMangas = new HashSet<UserAnimeManga>();
        }
        public int UserId {get; set;}
        public string Username {get; set;}

        public string Password {get; set;}

        public string Email {get; set;}

        public ICollection<UserAnimeManga> UserAnimeMangas {get; set;}
    }
}