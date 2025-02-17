using PeopleDirectory.Domain.Entities;

namespace PeopleDirectory.Domain.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<City>> GetAllCitiesAsync();
        Task<City?> GetByNameAsync(string cityName);
    }
}
