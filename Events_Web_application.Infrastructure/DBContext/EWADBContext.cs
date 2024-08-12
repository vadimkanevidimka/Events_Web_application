using Events_Web_application.Application.Services.AuthServices;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.DBContext
{
    public class EWADBContext : DbContext
    {
        public EWADBContext(DbContextOptions<EWADBContext> settings) : base(settings){}

        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<UsersEvents> UsersEvents { get; set; }
        public DbSet<EventCategory> VentCategories { get; set; }
        public DbSet<AccesToken> AccesTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventModelConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
