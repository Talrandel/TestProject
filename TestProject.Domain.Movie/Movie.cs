using TestProject.Common.Entities;
using System;

namespace TestProject.Domain.Movies
{
    public class Movie : EntityBase
    {
        public Movie(IdInt id)
            : base(id)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}