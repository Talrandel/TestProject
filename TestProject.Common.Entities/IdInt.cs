namespace TestProject.Common.Entities
{
    public class IdInt : IId<int>
    {
        private readonly int _value;

        public IdInt(int value)
        {
            _value = value;
        }
        public int Id => _value;

        public bool Equals(IdInt other)
        {
            return Id == other.Id;
        }

        public static implicit operator IdInt(int id)
        {
            return new IdInt(id);
        }
    }
}