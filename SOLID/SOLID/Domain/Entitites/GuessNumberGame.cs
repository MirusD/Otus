using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;
using SOLID.Domain.State;

namespace SOLID.Domain.Entitites
{
    class GuessNumberGame : IGame
    {
        private readonly INumberGenerator _numberGenerator;
        private readonly IGameSettings _settings;
        private int _targetNumber;
        private int _attempts;
        private IGameState _state;

        private bool IsGameOver()
        {
            if (_attempts <= 0)
            {
                _state.GameStatus = GameStatus.GameOver;
                return true;
            }

            return false;
        }

        public void ResetGame()
        {
            _targetNumber = _numberGenerator.Generate(_settings.MinValue, _settings.MaxValue);
            _attempts = _settings.MaxAttemps;
            _state.GameStatus = GameStatus.Start;
            _state.Attempts = _attempts;
            _state.TargetNumber = _targetNumber;
        }

        public GuessResult? MakeGuess(int guess)
        {
            if (IsGameOver())
            {
                _state.GuessResult = GuessResult.AttemptsOver;
                return _state.GuessResult;
            }
            else
            {
                _state.Attempts = --_attempts;

                _state.GuessResult = (guess.CompareTo(_targetNumber)) switch
                {
                    0 => GuessResult.Correct,
                    < 0 => GuessResult.TooLow,
                    > 0 => GuessResult.TooHigh,
                };
            }

            return _state.GuessResult;
        }

        public IGameState GetState()
        {
            return _state;
        }

        public GuessNumberGame(INumberGenerator numberGenerator, IGameSettings settings)
        {
            _numberGenerator = numberGenerator;
            _settings = settings;
            _state = new GameState();
            ResetGame();
        }
    }
}
