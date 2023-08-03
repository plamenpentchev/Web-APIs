using Microsoft.EntityFrameworkCore;

namespace MyBGList.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BoardGames_Domains>()
                .HasKey(p => new { p.BoardGameId, p.DomainId });
            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasKey(p => new { p.BoardGameId, p.MechanicId });
        }
    }
}
