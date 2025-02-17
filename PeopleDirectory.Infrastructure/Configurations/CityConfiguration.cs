using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDirectory.Domain.Entities;

namespace PeopleDirectory.Infrastructure.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            var fixedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(
                new City { Id = 1, Name = "Tbilisi", CreatedDate = fixedDate },
                new City { Id = 2, Name = "Kutaisi", CreatedDate = fixedDate },
                new City { Id = 3, Name = "Batumi", CreatedDate = fixedDate },
                new City { Id = 4, Name = "Kobuleti", CreatedDate = fixedDate },
                new City { Id = 5, Name = "Poti", CreatedDate = fixedDate }
            );
        }
    }
}
