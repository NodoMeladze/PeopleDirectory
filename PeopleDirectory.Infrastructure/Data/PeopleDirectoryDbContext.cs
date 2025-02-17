using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Domain.Entities;

namespace PeopleDirectory.Infrastructure.Data
{
    public class PeopleDirectoryDbContext(DbContextOptions<PeopleDirectoryDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public DbSet<RelatedPerson> RelatedPersons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleDirectoryDbContext).Assembly);
        }
    }
}
