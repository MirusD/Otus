namespace SOLID.Presentation.ConsoleApp.Commands.Interfaces
{
    interface ICommandFactory
    {
        public ICommand Create(CommandType type);
    }
}
