using TestProject.Application.Core.Repository;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;

namespace TestProject.Application.Movies
{
    public interface IMovieRepository : IRepositoryBase<Movie, IdInt>
    {
    }
}