using Microsoft.EntityFrameworkCore;

namespace WeebLibraryApi.Models
{
    public class WeebLibraryContext : DbContext
    {

        public DbSet<User> Users {get; set;}
        //public DbSet<Type> Types {get; set;}
        public DbSet<AnimeManga> AnimeMangas {get; set;}

        public DbSet<UserAnimeManga> UserAnimeMangas {get; set;}
        public WeebLibraryContext(DbContextOptions<WeebLibraryContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<AnimeManga>()
            //     .HasOne<Type>(t => t.Type)
            //     .WithMany(g => g.AnimeMangas)
            //     .HasForeignKey(t => t.TypeId);

            modelBuilder.Entity<UserAnimeManga>()
                .HasKey(x => new {x.UserId, x.AnimeMangaId,});

            

            modelBuilder.Entity<UserAnimeManga>()
                .HasOne<AnimeManga>(am => am.AnimeManga)
                .WithMany(sc => sc.UserAnimeMangas)
                .HasForeignKey(am => am.AnimeMangaId);

            modelBuilder.Entity<UserAnimeManga>()
                .HasOne<User>(u => u.User)
                .WithMany(sc => sc.UserAnimeMangas)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<AnimeManga>()
                .HasIndex(u => u.MalCode)
                .IsUnique();
        }
     

    }
}