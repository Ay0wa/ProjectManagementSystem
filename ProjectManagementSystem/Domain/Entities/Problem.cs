using ProjectManagementSystem.Domain.Interfaces;
using ProjectManagementSystem.Domain.Enums;
using System;

namespace ProjectManagementSystem.Domain.Entities
{
    public class Problem : IEntity
    {
        public Guid Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ProblemState Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime CreatedAt { get; }
        public string CreatedBy { get; }

        public Problem(string title, string description, Priority priority, string createdBy)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = ProblemState.New;
            Priority = priority;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void UpdateState(ProblemState newStatus)
        {
            Status = newStatus;
        }
    }
}