using System.Collections.Generic;
namespace WeebLibraryApi.Models
{
    public class Type
    {
        public int TypeId {get; set;}
        public string TypeName {get; set;}

        public List<AnimeManga> AnimeMangas {get; set;} = new List<AnimeManga>();
        
    }
}