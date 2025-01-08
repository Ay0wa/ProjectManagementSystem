namespace ProjectManagementSystem.Application.Commands
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}