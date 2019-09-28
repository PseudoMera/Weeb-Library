using Microsoft.EntityFrameworkCore;

namespace WeebLibraryApi.Models
{
    public class WeebLibraryContext : DbContext
    {
        public WeebLibraryContext(DbContextOptions<WeebLibraryContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeManga>()
                .HasOne<Type>(t => t.Type)
                .WithMany(g => g.AnimeMangas)
                .HasForeignKey(t => t.TypeId);

            modelBuilder.Entity<UserAnimeManga>()
                .HasKey(x => new {x.AnimeMangaId, x.UserId});

            
        }
        public DbSet<User> Users {get; set;}
        public DbSet<Type> Types {get; set;}
        public DbSet<AnimeManga> AnimeMangas {get; set;}

        public DbSet<UserAnimeManga> UserAnimeMangas {get; set;}

    }
}