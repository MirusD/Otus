using SOLID.Domain.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class AttemptsDown : ICommand
    {
        private IGameSettings _gameSettings;
        public AttemptsDown(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        public void Execute()
        {
            _gameSettings.MaxAttemps = Math.Clamp(_gameSettings.MaxAttemps - 1, 1, 100);
        }
    }
}
