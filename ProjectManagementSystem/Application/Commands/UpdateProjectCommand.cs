using ProjectManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementSystem.Application.Commands
{
    public class UpdateProjectCommand : ICommand
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly List<Project> projects;

        public UpdateProjectCommandHandler(List<Project> projects)
        {
            this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public void Handle(UpdateProjectCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var project = projects.FirstOrDefault(p => p.Id == command.ProjectId);
            
            if (project == null)
                throw new Exception($"Проект с ID {command.ProjectId} не найден");

            project.Update(command.Name, command.Description);
        }
    }
}