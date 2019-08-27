namespace TestProject.Common.Entities
{
    public interface IEntityBase<IId>
    {
        IId Id { get; }

        bool Equals(IId other);
    }
}