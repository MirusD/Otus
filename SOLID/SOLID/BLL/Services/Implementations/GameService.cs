using SOLID.BLL.Domain;
using SOLID.BLL.Domain.Settings;
using SOLID.BLL.Domain.Settings.Interfaces;
using SOLID.BLL.Services.Abstractions;

namespace SOLID.BLL.Services.Implementations
{
    class GameService : IGameService
    {
        private Game _game;
        
        public GameService()
        {
            _game = new Game() 
            { 
                Range = new RangeNumbers(), 
                Attempts = 10 
            };
        }

        public IGameSettings GetSettings()
        {
            return new GameSettings { Attempts = _game.Attempts, Range = _game.Range };
        }

        public void SetSettings(IGameSettings gameSettings)
        {
            if (_game.IsStarted == false)
            {
                _game.Attempts = gameSettings.Attempts;
                _game.Range = gameSettings.Range;
            }
        }

        public void Start()
        {
            Random rnd = new Random();
            _game.Number = rnd.Next(_game.Range.MinValue, _game.Range.MaxValue);
            _game.AttemptsCount = 0;
        }

        public int? GuessNumber(int number)
        {
            if (GetAttemptCount() > 1)
            {
                _game.AttemptsCount++;

                if (number > _game.Number) return 1;
                else if (number < _game.Number) return -1;

                return 0;
            }
            else return null;
        }

        public int GetAttemptCount()
        {
            return _game.Attempts - _game.AttemptsCount;
        }

        public int GetAnswer()
        {
            return _game.Number;
        }
    }
}
