using Microsoft.EntityFrameworkCore;

namespace Events_Web_application_DataBase
{
    public class EWADBContext : DbContext
    {
        public EWADBContext(DbContextOptions<EWADBContext> settings) : base(settings)
        {
            if (!Database.CanConnect()) Database.EnsureCreated();
            else Database.Migrate();
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<UsersEvents> UsersEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithMany(e => e.UserEvents)
                .UsingEntity<UsersEvents>(
                    l => l.HasOne<Participant>().WithMany().HasForeignKey(e => e.ParticipantId),
                    r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.EventId));

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
