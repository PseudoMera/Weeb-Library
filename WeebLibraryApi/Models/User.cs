using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeebLibraryApi.Models
{
    public class User
    {
    
        [Key]
        public int UserId {get; set;}
        
        [Required]
        public string Username {get; set;}

        [Required]
        [MinLength(8)]
        public string Password {get; set;}

        [Required]
        [MaxLength(100)]
        public string Email {get; set;}


        public DateTime RegisterDate {get; set;}

       public ICollection<UserAnimeManga> UserAnimeMangas {get; set;}
    }
}