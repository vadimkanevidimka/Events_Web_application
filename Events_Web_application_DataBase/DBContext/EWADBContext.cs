using Events_Web_application.Application.Services.AuthServices;
using Events_Web_application.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.DBContext
{
    public class EWADBContext : DbContext
    {
        ModelFluenAPI _fluentApiAnnotation;
        public EWADBContext(DbContextOptions<EWADBContext> settings) : base(settings)
        { Database.EnsureCreated(); }

        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<UsersEvents> UsersEvents { get; set; }
        public DbSet<EventCategory> VentCategories { get; set; }
        public DbSet<AccesToken> AccesTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _fluentApiAnnotation = new ModelFluenAPI();
            _fluentApiAnnotation.EntityModelAnnotation(modelBuilder)
                .UserModelAnnotation(modelBuilder)
                .ParticipantModelAnnotation(modelBuilder);
        }
    }
}
