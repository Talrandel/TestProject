using TestProject.Application.Core.Repository;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;
using TestProject.Domain.Persons;

namespace TestProject.Application.Persons
{
    public class PersonRepository : RepositoryBase<Person, IdInt>, IPersonRepository
    {
        public PersonRepository(IDbContext<Person, IdInt> context)
            : base(context)
        {
        }
    }
}