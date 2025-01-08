namespace ProjectManagementSystem.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        string CreatedBy { get; }
    }
}