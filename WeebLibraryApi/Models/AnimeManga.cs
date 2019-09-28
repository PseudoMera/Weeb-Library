using System.Collections.Generic;

namespace WeebLibraryApi.Models
{
    public class AnimeManga
    {
        public int AnimeMangaId {get; set;}
        public string ImageURL {get; set;}
        public string Title {get; set;}

        public int TypeId {get; set;}
        public Type Type {get; set;}
        public int MalCode {get; set;}

        public List<UserAnimeManga> UserAnimeMangas {get; set;}

    }
}