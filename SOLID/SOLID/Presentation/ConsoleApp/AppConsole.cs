using SOLID.Application.Interfaces;
using SOLID.Presentation.ConsoleApp.Controller;
using SOLID.Presentation.ConsoleApp.Menu;
using SOLID.Presentation.ConsoleApp.Sound;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp
{
    class AppConsole
    {
        private readonly ConsoleSound _consoleSound;
        private readonly GamePlayViewModel _gamePlayViewModel;
        private readonly IGameService _gameService;
        private readonly MenuBuilder _menuBuilder;
        private readonly GameController _gameController;

        public AppConsole(
            GamePlayViewModel gamePlayViewModel, 
            ConsoleSound consoleSound, 
            MenuBuilder menuBuilder, 
            IGameService gameService, 
            GameController gameController)
        {
            _gamePlayViewModel = gamePlayViewModel;
            _consoleSound = consoleSound;
            _menuBuilder = menuBuilder;
            _gameService = gameService;
            _gameController = gameController;
        }

        public void Init()
        {
            _gameService.Subscribe(_consoleSound);
            _gameService.Subscribe(_gamePlayViewModel);
            _gameService.Subscribe(_gameController);

            _consoleSound.PlayMainMenuSound();
            _menuBuilder.BuildMainMenu().Show();
        }
    }
}
