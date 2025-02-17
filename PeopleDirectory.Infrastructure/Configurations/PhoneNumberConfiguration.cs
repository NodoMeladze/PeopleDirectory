using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Infrastructure.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasQueryFilter(ph => !ph.IsDeleted);

            builder.Property(ph => ph.Type)
                   .HasConversion(
                        v => v.ToString(),
                        v => (PhoneType)Enum.Parse(typeof(PhoneType), v))
                   .HasMaxLength(10);

            builder.HasOne(ph => ph.Person)
                   .WithMany(p => p.PhoneNumbers)
                   .HasForeignKey(ph => ph.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
