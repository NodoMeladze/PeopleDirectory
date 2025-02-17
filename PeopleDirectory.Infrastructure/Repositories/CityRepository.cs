using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Interfaces;
using PeopleDirectory.Infrastructure.Data;

namespace PeopleDirectory.Infrastructure.Repositories
{
    public class CityRepository(PeopleDirectoryDbContext context) : Repository<City>(context), ICityRepository
    {
        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<City?> GetByNameAsync(string cityName)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Name == cityName);
        }
    }
}
