using SOLID.Application.Interfaces;
using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;

namespace SOLID.Application.Services
{
    class GuessNumberGameService : IGameService
    {
        private IGame _game;
        private readonly IGameSettings _gameSettings;
        private readonly IGameFactory _gameFactory;

        public GuessNumberGameService(IGameFactory gameFactory, IGameSettings gameSettings)
        {
            _gameFactory = gameFactory;
            _gameSettings = gameSettings;
        }


        public GuessResult MakeGuess(int guess)
        {
            return _game.MakeGuess(guess);
        }

        public void StartGame()
        {
            _game = _gameFactory.CreateGame(_gameSettings);
        }

        public void ResetGame()
        {
            if (_game == null)
            {
                throw new InvalidOperationException("Игра не запущена. Нельзя выполнить сброс.");
            }

            _game.ResetGame();
        }

        public int GetAttempt()
        {
            return _game.Attemps;
        }

        public int GetTargetNumber()
        {
            return _game.TargetNumber;
        }
    }
}
