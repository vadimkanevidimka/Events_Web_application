using Events_Web_application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events_Web_application.Infrastructure.DBContext.EntitiesConfigurations
{
    internal class ParticipantEntityConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(200);
            builder.Property(p => p.LastName).HasMaxLength(200);
        }
    }
}
