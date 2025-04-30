using SOLID.Presentation.ConsoleApp.Commands.Interfaces;
using SOLID.Presentation.ConsoleApp.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands.Factories
{
    class CommandFactory : ICommandFactory
    {
        private readonly IGameController _gameController;

        public CommandFactory(IGameController gameController)
        {
            _gameController = gameController;
        }

        public ICommand CreateStartGameCommand() =>
            new ActionCommand(() => _gameController.StartGame());

        public ICommand CreateExitGameCommand() =>
            new ActionCommand(() => _gameController.ExitGame());

        public ICommand CreateChangeVolumeCommand(int delta) =>
            new ActionCommand(() => _gameController.ChangeVolume(delta));

        public ICommand CreateToggleSoundMuteCommand() =>
            new ActionCommand(() => _gameController.ToggleMute());

        public ICommand CreateChangeAttemptsCommand(int delta) =>
            new ActionCommand(() => _gameController.ChangeAttempts(delta));

        public ICommand CreateChangeMinValueCommand(int delta) =>
            new ActionCommand(() => _gameController.ChangeMinValue(delta));

        public ICommand CreateChangeMaxValueCommand(int delta) =>
            new ActionCommand(() => _gameController.ChangeMaxValue(delta));
    }
}
