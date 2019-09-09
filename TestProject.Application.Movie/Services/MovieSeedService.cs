using System;
using System.Threading.Tasks;
using TestProject.Application.Core.Services;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;

namespace TestProject.Application.Movies.Services
{
    public class MovieSeedService : ISeedDataService
    {
        private readonly IDbContext<Movie, IdInt> _context;
        public MovieSeedService(IDbContext<Movie, IdInt> context)
        {
            _context = context;
        }

        public Task Clear()
        {
            return _context.Clear();
        }

        public async Task Initialize()
        {
            await _context.CreateAsync(new Movie(1) { Description = "1", Name = "1", ReleaseDate = new DateTime(1970, 1, 1)})
                .ConfigureAwait(false);
            await _context.CreateAsync(new Movie(2) { Description = "2", Name = "2", ReleaseDate = new DateTime(1981, 1, 1) })
                .ConfigureAwait(false);
            await _context.CreateAsync(new Movie(3) { Description = "3", Name = "3", ReleaseDate = new DateTime(1994, 1, 1) })
                .ConfigureAwait(false);
            await _context.CreateAsync(new Movie(4) { Description = "4", Name = "4", ReleaseDate = new DateTime(2012, 1, 1) })
                .ConfigureAwait(false);
        }
    }
}