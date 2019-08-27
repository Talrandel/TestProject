using System;

namespace TestProject.Common.Entities
{
    public interface IId<T> where T : IEquatable<T>
    {
    }
}