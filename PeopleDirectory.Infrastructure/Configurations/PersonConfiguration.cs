using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(p => p.Gender)
                   .HasConversion(
                        v => v.ToString(),
                        v => (Gender)Enum.Parse(typeof(Gender), v))
                   .HasMaxLength(10);

            builder.HasOne(p => p.City)
                   .WithMany(c => c.Persons)
                   .HasForeignKey(p => p.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PhoneNumbers)
                   .WithOne(pn => pn.Person)
                   .HasForeignKey(pn => pn.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.RelatedPersons)
                   .WithOne(rp => rp.Person)
                   .HasForeignKey(rp => rp.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
