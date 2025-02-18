using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Interfaces;
using PeopleDirectory.Infrastructure.Data;

namespace PeopleDirectory.Infrastructure.Repositories
{
    public class PersonRepository(PeopleDirectoryDbContext context) : Repository<Person>(context), IPersonRepository
    {
        public async Task<(List<Person> Persons, int TotalCount)> SearchPersonsAsync(
            string? firstName, string? lastName, string? personalId, int pageNumber, int pageSize, bool detailed = false)
        {
            IQueryable<Person> query = _dbSet;

            if (detailed)
            {
                query = query.Include(p => p.PhoneNumbers)
                             .Include(p => p.City)
                             .Include(p => p.RelatedPersons)
                             .ThenInclude(rp => rp.Related);
            }

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{firstName}%"));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(p => EF.Functions.Like(p.LastName, $"%{lastName}%"));

            if (!string.IsNullOrWhiteSpace(personalId))
                query = query.Where(p => EF.Functions.Like(p.PersonalId, $"%{personalId}%"));

            var totalCount = await query.CountAsync();

            var persons = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (persons, totalCount);
        }

        public async Task<Person?> GetPersonFullDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.PhoneNumbers)
                .Include(p => p.City)
                .Include(p => p.RelatedPersons)
                .ThenInclude(rp => rp.Related)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _dbSet
                .Include(p => p.PhoneNumbers)
                .Include(p => p.City)
                .Include(p => p.RelatedPersons)
                    .ThenInclude(rp => rp.Related)
                .ToListAsync();
        }

        public async Task<Person?> GetByPersonalIdAsync(string personalId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.PersonalId == personalId);
        }
    }
}
