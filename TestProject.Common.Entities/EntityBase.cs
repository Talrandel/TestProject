
namespace TestProject.Common.Entities
{
    public class EntityBase : IEntityBase<IdInt>
    {
        private readonly IdInt _id;
        public EntityBase(IdInt id)
        {
            _id = id;
        }

        public IdInt Id => _id;

        public bool Equals(IdInt other)
        {
            return _id.Equals(other);
        }
    }
}