using System.Threading.Tasks;
using TestProject.Application.Core.Services;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;
using TestProject.Domain.Persons;

namespace TestProject.Application.Persons.Services
{
    public class PersonSeedService : ISeedDataService
    {
        private readonly IDbContext<Person, IdInt> _context;
        public PersonSeedService(IDbContext<Person, IdInt> context)
        {
            _context = context;
        }

        public Task Clear()
        {
            return _context.Clear();
        }

        public async Task Initialize()
        {
            await _context.CreateAsync(new Person(1) { FirstName = "1", LastName = "1", FavMoviesIds = new IdInt[] { 1 } })
                .ConfigureAwait(false);
            await _context.CreateAsync(new Person(2) { FirstName = "2", LastName = "2", FavMoviesIds = new IdInt[] { 2, 3 } })
                .ConfigureAwait(false);
            await _context.CreateAsync(new Person(3) { FirstName = "3", LastName = "3", FavMoviesIds = new IdInt[] { 3, 4 } })
                .ConfigureAwait(false);
            await _context.CreateAsync(new Person(4) { FirstName = "4", LastName = "4", FavMoviesIds = new IdInt[] { 1, 3 } })
                .ConfigureAwait(false);
        }
    }
}