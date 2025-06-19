using SOLID.Application.Interfaces;
using SOLID.Domain.Interfaces;

namespace SOLID.Application.Services
{
    class GuessNumberGameService : IGameService
    {
        private IGame? _game;
        private readonly IGameSettings _gameSettings;
        private readonly IGameFactory _gameFactory;

        private readonly List<IGameObserver> _observers = new();

        public void Subscribe(IGameObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IGameObserver observer)
        {
            _observers.Remove(observer);
        }

        private void Notify(IGameState state)
        {
            foreach (var observer in _observers)
                observer.OnGameStateChanged(state);
        }

        public GuessNumberGameService(IGameFactory gameFactory, IGameSettings gameSettings)
        {
            _gameFactory = gameFactory;
            _gameSettings = gameSettings;
        }

        public void MakeGuess(int guess)
        {
            if (_game is IGame)
            {
                _game.MakeGuess(guess);
                Notify(_game.GetState());
            }
        }

        public void StartGame()
        {
            _game = _gameFactory.CreateGame(_gameSettings);
            Notify(_game.GetState());
        }

        public void ResetGame()
        {
            if (_game == null)
            {
                throw new InvalidOperationException("Игра не запущена. Нельзя выполнить сброс.");
            }

            _game.ResetGame();
            Notify(_game.GetState());
        }

    }
}
