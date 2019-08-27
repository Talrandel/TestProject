using TestProject.Application.Core.Repository;
using TestProject.Common.Entities;
using TestProject.Domain.Persons;

namespace TestProject.Application.Persons
{
    public interface IPersonRepository : IRepositoryBase<Person, IdInt>
    {
    }
}