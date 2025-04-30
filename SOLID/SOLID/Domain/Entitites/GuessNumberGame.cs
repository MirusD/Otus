using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;

namespace SOLID.Domain.Entitites
{
    class GuessNumberGame : IGame
    {
        private readonly INumberGenerator _numberGenerator;
        private readonly IGameSettings _settings;
        private int _targetNumber;
        private int _attempts;

        public GuessNumberGame(INumberGenerator numberGenerator, IGameSettings settings)
        {
            _numberGenerator = numberGenerator;
            _settings = settings;
            ResetGame();
        }

        public int Attemps => _attempts;
        public int TargetNumber => _targetNumber;

        public void ResetGame()
        {
            _targetNumber = _numberGenerator.Generate(_settings.MinValue, _settings.MaxValue);
            _attempts = _settings.MaxAttemps;
        }

        public GuessResult MakeGuess(int guess)
        {
            if (IsGameOver())
            {
                return GuessResult.GameOver;
            }

            _attempts--;

            return guess switch
            {
                _ when guess == _targetNumber => GuessResult.Correct,
                _ when guess < _targetNumber => GuessResult.TooLow,
                _ when guess > _targetNumber => GuessResult.TooHigh,
                _ => throw new ArgumentOutOfRangeException(nameof(guess), "Неверный аргумент guess")
            };
        }

        public bool IsGameOver()
        {
            return _attempts <= 0;
        }
    }
}
