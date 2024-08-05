using Events_Web_application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Events_Web_application.Infrastructure.DBContext.EntitiesConfigurations
{
    internal class EventModelConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Location).HasMaxLength(300);

            builder.HasMany(e => e.Participants)
                .WithMany(e => e.UserEvents)
                .UsingEntity<UsersEvents>(
                    l => l.HasOne<Participant>().WithMany().HasForeignKey(e => e.ParticipantId),
                    r => r.HasOne<Event>().WithMany().HasForeignKey(e => e.EventId));

            builder.HasOne(c => c.Category).WithMany(c => c.Events);

            builder.Navigation(e => e.Participants).AutoInclude();
            //builder.Navigation(e => e.EventImage).AutoInclude();
            builder.Navigation(e => e.Category).AutoInclude();
        }
    }
}
