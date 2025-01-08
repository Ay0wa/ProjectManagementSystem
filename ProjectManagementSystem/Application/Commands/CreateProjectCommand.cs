using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Application.Commands
{
    public class CreateProjectCommand : ICommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly List<Project> projects;

        public CreateProjectCommandHandler(List<Project> projects)
        {
            this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public void Handle(CreateProjectCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Name cannot be empty");
                
            if (string.IsNullOrWhiteSpace(command.Description))
                throw new ArgumentException("Description cannot be empty");
                
            if (string.IsNullOrWhiteSpace(command.CreatedBy))
                throw new ArgumentException("CreatedBy cannot be empty");

            var project = new Project(
                command.Name,
                command.Description,
                command.CreatedBy
            );

            projects.Add(project);
        }
    }
}