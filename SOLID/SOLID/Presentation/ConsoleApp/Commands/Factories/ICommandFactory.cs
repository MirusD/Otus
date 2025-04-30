using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands.Factories
{
    interface ICommandFactory
    {
        ICommand CreateStartGameCommand();
        ICommand CreateExitGameCommand();
        ICommand CreateChangeVolumeCommand(int delta);
        ICommand CreateToggleSoundMuteCommand();
        ICommand CreateChangeAttemptsCommand(int delta);
        ICommand CreateChangeMinValueCommand(int delta);
        ICommand CreateChangeMaxValueCommand(int delta);

    }
}
