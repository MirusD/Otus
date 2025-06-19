using SOLID.Application.Interfaces;
using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;
using SOLID.Presentation.ConsoleApp.Menu;

namespace SOLID.Presentation.ConsoleApp.ViewModels
{
    class GamePlayViewModel : IGameObserver
    {
        private readonly MenuBuilder _menuBuilder;

        public GamePlayViewModel(MenuBuilder menuBuilder)
        {
            _menuBuilder = menuBuilder;
        }

        public void OnGameStateChanged(IGameState state)
        {
            if (state.GameStatus == GameStatus.Start && state.GuessResult == null)
            {
                Console.WriteLine($"Игра началась! У вас есть {state.Attempts} попыток");
            }

            if (state.GuessResult == GuessResult.Correct)
            {
                _menuBuilder.BuildGameEndMenu($"Вы победили!").Show();
            } else if (state.Attempts == 0)
            {
                _menuBuilder.BuildGameEndMenu($"Вы проиграли. Я загадал число {state.TargetNumber}").Show();
            }

            if (state.GuessResult != null && state.Attempts != 0)
            {
                string str = state.GuessResult == GuessResult.TooHigh ? "больше" : "меньше";
                Console.WriteLine($"Ваше число {str}. Осталось попыток: {state.Attempts}");
            }
        }
    }
}
