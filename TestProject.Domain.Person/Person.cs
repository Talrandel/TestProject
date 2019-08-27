using TestProject.Common.Entities;
using System.Collections.Generic;

namespace TestProject.Domain.Persons
{
    public class Person : EntityBase
    {
        public Person(IdInt id)
            : base(id)
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO: в классе сущности сделать список связанных объектов?
        public IdInt[] FavMoviesIds { get; set; }
    }
}