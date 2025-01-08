using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Enums;

namespace ProjectManagementSystem.Application.Commands
{
    public class UpdateProblemCommand : ICommand
    {
        public Guid ProblemId { get; set; }
        public ProblemState? State { get; set; }
    }

    public class UpdateProblemCommandHandler : ICommandHandler<UpdateProblemCommand>
    {
        private readonly List<Project> projects;

        public UpdateProblemCommandHandler(List<Project> projects)
        {
            this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public void Handle(UpdateProblemCommand command)
        {
            var problem = projects
                .SelectMany(p => p.Problems)
                .FirstOrDefault(t => t.Id == command.ProblemId);

            if (problem == null)
                throw new Exception($"Задача с ID {command.ProblemId} не найдена");

            if (command.State == null)
            {
                throw new ArgumentNullException(nameof(command.State), "Статус не может быть null");
            }

            if (!Enum.IsDefined(typeof(ProblemState), command.State))
            {
                throw new ArgumentException($"Недопустимый статус: {command.State}");
            }
            
            problem.UpdateState(command.State.Value);
        }
    }
}