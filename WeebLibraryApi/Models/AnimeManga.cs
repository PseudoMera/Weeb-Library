using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeebLibraryApi.Models
{
    public class AnimeManga
    {


        [Key]
        public int AnimeMangaId {get; set;}
        
        public string ImageURL {get; set;}
        
        [MaxLength(100)]
        public string Title {get; set;}
        
        [ForeignKey("Type")]
        public int TypeId {get; set;}
        public Type Type {get; set;}

        // public string Type {get; set;}
        [Required]
        public int MalCode {get; set;}

        public ICollection<UserAnimeManga> UserAnimeMangas {get; set;}

    }
}