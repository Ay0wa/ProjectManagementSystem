using ProjectManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Domain.Entities
{
    public class Project : IEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; }
        public string CreatedBy { get; }
        public List<Problem> Problems { get; }

        public Project(string name, string description, string createdBy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            Problems = new List<Problem>();
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void AddProblem(Problem Problem)
        {
            Problems.Add(Problem);
        }
    }
}