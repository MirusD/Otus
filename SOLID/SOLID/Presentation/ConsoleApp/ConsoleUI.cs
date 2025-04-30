using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;
using SOLID.Presentation.ConsoleApp.Events;
using SOLID.Presentation.ConsoleApp.Interfaces;
using SOLID.Presentation.ConsoleApp.Menu;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp
{
    class ConsoleUI
    {
        private readonly MenuBuilder _menuBuilder;

        public ConsoleUI(IGameController gameController, MenuBuilder menuBuilder)
        {
            gameController.Subscribe(async viewModel => await Task.Run(() => DisplayGamePlay(viewModel)));
            _menuBuilder = menuBuilder;
        } 

        public void Start()
        {
            _menuBuilder.BuildMainMenu().Show();
        }


        public void DisplayGamePlay(GamePlayViewModel gamePlayViewModel)
        {
            if (gamePlayViewModel.GameStatus == GuessResult.GameOver)
            {
                _menuBuilder.BuildGameEndMenu($"Вы проиграли. Я загадал число {gamePlayViewModel.TargetNumber}").Show();
                return;
            }
            else if (gamePlayViewModel.GameStatus == GuessResult.Correct)
            {
                _menuBuilder.BuildGameEndMenu($"Вы победили!").Show();
                return;
            }

            string str = gamePlayViewModel.GameStatus == GuessResult.TooHigh ? "больше" : "меньше";
            Console.WriteLine($"Ваше число {str}. Осталось попыток: {gamePlayViewModel.Attempts}");
        }
    } 
}
