using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeebLibraryApi.Models
{
    public class Type
    {
        [Key]
        public int TypeId {get; set;}

        [MaxLength(50)]
        public string TypeName {get; set;}

        public List<AnimeManga> AnimeMangas {get; set;} = new List<AnimeManga>();
        
    }
}