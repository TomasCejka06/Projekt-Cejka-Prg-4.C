using Microsoft.EntityFrameworkCore;
using CejkaTomas_UFC_APP.Models;

namespace CejkaTomas_UFC_APP.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fighter> Fighters { get; set; } = null!;
        public DbSet<Fights> Fights { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Fights -> Fighter (Red corner)
            modelBuilder.Entity<Fights>()
                .HasOne(f => f.FighterRed)
                .WithMany() // nechceme mapovat kolekce v Fighter
                .HasForeignKey(f => f.FighterRedId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Fights -> Fighter (Blue corner)
            modelBuilder.Entity<Fights>()
                .HasOne(f => f.FighterBlue)
                .WithMany()
                .HasForeignKey(f => f.FighterBlueId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Fights -> Fighter (Winner)
            modelBuilder.Entity<Fights>()
                .HasOne(f => f.Winner)
                .WithMany()
                .HasForeignKey(f => f.WinnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>().ToTable("Users");


            // pokud mas v Fighter tyhle kolekce a nechces je resit:
            modelBuilder.Entity<Fighter>().Ignore(x => x.FightFighterReds);
            modelBuilder.Entity<Fighter>().Ignore(x => x.FightFighterBlues);
        }
    }
}
