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

        public bool Equals(int other)
        {
            return _value == other;
        }

        public static implicit operator IdInt(int id)
        {
            return new IdInt(id);
        }
    }
}