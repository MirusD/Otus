using SOLID.Domain.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class MinValueUp : ICommand
    {
        private IGameSettings _gameSettings;
        public MinValueUp(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        public void Execute()
        {
            _gameSettings.MinValue = Math.Clamp(_gameSettings.MinValue + 1, 0, 100);
        }
    }
}
