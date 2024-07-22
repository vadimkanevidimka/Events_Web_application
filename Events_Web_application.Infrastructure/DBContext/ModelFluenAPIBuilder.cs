using Events_Web_application.Domain.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Events_Web_application.Infrastructure.DBContext
{
    internal class ModelFluenAPI
    {
        internal ModelFluenAPI EntityModelAnnotation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().Property(p => p.Title).HasMaxLength(200);
            modelBuilder.Entity<Event>().Property(p => p.Description).HasMaxLength(1000);
            modelBuilder.Entity<Event>().Property(p => p.Location).HasMaxLength(300);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Participants)
                .WithMany(e => e.UserEvents)
                .UsingEntity<UsersEvents>(
                    l => l.HasOne<Participant>().WithMany().HasForeignKey(e => e.ParticipantId),
                    r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.EventId));

            modelBuilder.Entity<Event>()
                .HasOne(c => c.Category).WithMany(c => c.Events);

            modelBuilder.Entity<Event>().Navigation(e => e.Participants).AutoInclude();
            modelBuilder.Entity<Event>().Navigation(e => e.EventImage).AutoInclude();
            modelBuilder.Entity<Event>().Navigation(e => e.Category).AutoInclude();
            

            return this;
        }

        internal ModelFluenAPI ParticipantModelAnnotation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>().Property(p => p.FirstName).HasMaxLength(200);
            modelBuilder.Entity<Participant>().Property(p => p.LastName).HasMaxLength(200);
            return this;
        }

        internal ModelFluenAPI UserModelAnnotation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(100);
            modelBuilder.Entity<User>().Navigation(e => e.Participant).AutoInclude();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>().HasOne(u => u.AsscesToken);
            return this;
        }
    }
}
