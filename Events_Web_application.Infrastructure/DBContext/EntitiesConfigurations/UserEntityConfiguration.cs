using Events_Web_application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Events_Web_application.Infrastructure.DBContext.EntitiesConfigurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Navigation(e => e.Participant).AutoInclude();
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.HasOne(u => u.AsscesToken);
        }
    }
}
