using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectManagementSystem.Application.Commands
{
    public class CreateProblemCommand : ICommand
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CreateProblemCommandHandler : ICommandHandler<CreateProblemCommand>
    {
        private readonly List<Project> projects;

        public CreateProblemCommandHandler(List<Project> projects)
        {
            this.projects = projects;
        }

        public void Handle(CreateProblemCommand command)
        {
            var project = projects.FirstOrDefault(p => p.Id == command.ProjectId);
            if (project == null)
                throw new Exception("Project not found");

            var Problem = new Problem(
                command.Title,
                command.Description,
                command.Priority,
                command.CreatedBy
            );
            
            project.AddProblem(Problem);
        }
    }
}