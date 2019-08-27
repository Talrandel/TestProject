using TestProject.Application.Core.Repository;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;

namespace TestProject.Application.Movies
{
    public class MovieRepository : RepositoryBase<Movie, IdInt>, IMovieRepository
    {
        public MovieRepository(IDbContext<Movie, IdInt> context)
            : base(context)
        {
        }
    }
}