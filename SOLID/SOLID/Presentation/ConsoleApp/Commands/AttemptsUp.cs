using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class AttemptsUp : ICommand
    {
        private IGameSettings _gameSettings;
        public AttemptsUp(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        public void Execute()
        {
            _gameSettings.MaxAttemps = Math.Clamp(_gameSettings.MaxAttemps + 1, 1, 100);
        }
    }
}
