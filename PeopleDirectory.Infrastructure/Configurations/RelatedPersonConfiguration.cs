using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Infrastructure.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.HasQueryFilter(rp => !rp.IsDeleted);

            builder.Property(rp => rp.ConnectionType)
                   .HasConversion(
                        v => v.ToString(),
                        v => (ConnectionType)Enum.Parse(typeof(ConnectionType), v))
                   .HasMaxLength(15);

            builder.HasOne(rp => rp.Person)
                   .WithMany(p => p.RelatedPersons)
                   .HasForeignKey(rp => rp.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rp => rp.Related)
                   .WithMany()
                   .HasForeignKey(rp => rp.RelatedPersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
