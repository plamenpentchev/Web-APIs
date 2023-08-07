using Microsoft.EntityFrameworkCore;

namespace MyBGList_ApiVersion.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BoardGame> BoradGames => Set<BoardGame>();
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();

        public DbSet<BoardGames_Domains> BoardGames_Domains => Set<BoardGames_Domains>();

        public DbSet<BoardGames_Mechanics> BoardGames_Mechanics => Set<BoardGames_Mechanics>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoardGames_Domains>()
                .HasKey(p => new { p.BoardGameId, p.DomainId });

            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(x => x.BoardGame)
                .WithMany(x => x.BoardGames_Domains)
                .HasForeignKey(x => x.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(x => x.Domain)
                .WithMany(x => x.BoardGames_Domains)
                .HasForeignKey(x => x.DomainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasKey(p => new { p.BoardGameId, p.MechanicId });

            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne(x => x.BoardGame)
                .WithMany(x => x.BoardGames_Mechanics)
                .HasForeignKey(x => x.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne (x => x.Mechanic)
                .WithMany(x => x.BoardGames_Mechanics)
                .HasForeignKey (x => x.MechanicId)
                .IsRequired()
                .OnDelete (DeleteBehavior.Cascade);



        }
    }
}
