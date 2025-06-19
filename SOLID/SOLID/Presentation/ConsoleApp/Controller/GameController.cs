using SOLID.Application.Interfaces;
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Presentation.ConsoleApp.Enums;

namespace SOLID.Presentation.ConsoleApp.Controller
{
    class GameController : IGameObserver
    {
        private readonly IGameService _gameService;

        public GameController
            (
                IGameService gameService, 
                ISoundPlayer<SoundGameTrack> soundPlayer
                
            )
        {
            _gameService = gameService;
        }

        public void StartGame()
        {
            EnterNumber().Wait();
        }

        public async Task EnterNumber()
        {
            Console.Write("Введите число:");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                await GuessNumber(number);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }

        private async Task GuessNumber(int guess)
        {
            _gameService.MakeGuess(guess);
        }

        public void OnGameStateChanged(IGameState state)
        {
            if (state.Attempts != 0) EnterNumber().Wait();
        }
    }
}
