using SOLID.Domain.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class MaxValueUp : ICommand
    {
        private IGameSettings _gameSettings;
        public MaxValueUp(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        public void Execute()
        {
            _gameSettings.MaxValue = Math.Clamp(_gameSettings.MaxValue + 1, 0, 100);
        }
    }
}
