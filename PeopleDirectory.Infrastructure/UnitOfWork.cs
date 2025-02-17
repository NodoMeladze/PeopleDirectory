using PeopleDirectory.Domain.Interfaces;
using PeopleDirectory.Infrastructure.Data;

namespace PeopleDirectory.Infrastructure
{
    public sealed class UnitOfWork(PeopleDirectoryDbContext context) : IUnitOfWork
    {
        private readonly PeopleDirectoryDbContext _context = context;
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
