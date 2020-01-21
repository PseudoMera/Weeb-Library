using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeebLibraryApi.Models
{
    public class UserAnimeManga
    {
        [ForeignKey("AnimeManga")]
        public int AnimeMangaId {get; set;}
        public AnimeManga AnimeManga {get; set;}

        
        [ForeignKey("User")]
        public int UserId {get; set;}
        public User User {get; set;}
        
    }
}