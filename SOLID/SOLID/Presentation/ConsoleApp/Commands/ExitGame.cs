using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class ExitGame : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Выход из игры.");
            Environment.Exit(0);
        }
    }
}
