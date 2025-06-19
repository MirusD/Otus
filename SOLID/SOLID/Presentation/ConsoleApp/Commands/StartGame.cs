using SOLID.Application.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;
using SOLID.Presentation.ConsoleApp.Controller;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class StartGame : ICommand
    {
        private readonly IGameService _gameService;
        private readonly GameController _gameController;

        public StartGame
            (
                IGameService gameService,
                GameController gameController
            )
        {
            _gameService = gameService;
            _gameController = gameController;
        }
        public void Execute()
        {
            _gameService.StartGame();
            _gameController.StartGame();
        }
    }
}
