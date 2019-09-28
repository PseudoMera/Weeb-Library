namespace WeebLibraryApi.Models
{
    public class UserAnimeManga
    {
        public int AnimeMangaId {get; set;}
        public int UserId {get; set;}

        public AnimeManga AnimeManga {get; set;}
        public User User {get; set;}
        
    }
}