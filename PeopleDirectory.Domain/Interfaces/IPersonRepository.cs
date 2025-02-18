using PeopleDirectory.Domain.Entities;

namespace PeopleDirectory.Domain.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<(List<Person> Persons, int TotalCount)> SearchPersonsAsync(string? firstName, string? lastName, string? personalId, int pageNumber, int pageSize, bool detailed = false);
        Task<Person?> GetPersonFullDetailsAsync(int id);
        Task<Person?> GetByPersonalIdAsync(string personalId);
        Task<List<Person>> GetAllPersonsAsync();
    }
}
