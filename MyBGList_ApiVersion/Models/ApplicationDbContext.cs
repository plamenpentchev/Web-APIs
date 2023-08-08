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

        public DbSet<Publisher>? Publishers  => Set<Publisher>();

        public DbSet<BoardGames_Categories> BoardGames_Categories => Set<BoardGames_Categories>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //many-to many, many board games many domains 
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

            //many-to many, many board games many mechanics 
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

            //One-to-many One publisher to many board games
            modelBuilder.Entity<BoardGame>()
                .HasOne(x => x.Publisher)
                .WithMany(x => x.BoardGames )
                .HasForeignKey(x => x.PublisherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //many-to many, many board games many categories 
            modelBuilder.Entity<BoardGames_Categories>()
                .HasKey(p => new { p.CategoryId, p.BoardGameId });

            modelBuilder.Entity<BoardGames_Categories>()
                .HasOne(x => x.Category)
                .WithMany(X => X.BoardGames_Categories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Categories>()
                .HasOne(x => x.BoardGame)
                .WithMany(X => X.BoardGames_Categories)
                .HasForeignKey(x => x.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
