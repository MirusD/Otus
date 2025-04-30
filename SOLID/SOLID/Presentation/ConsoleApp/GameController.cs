using SOLID.Application.Interfaces;
using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Infrastructure.Settings.Interfaces;
using SOLID.Presentation.ConsoleApp.Enums;
using SOLID.Presentation.ConsoleApp.Interfaces;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp
{
    class GameController : IGameController
    {
        private readonly IGameService _gameService;
        private readonly IGameSettings _gameSettings;
        private readonly ISoundSettings _soundSettings;
        private readonly ISoundPlayer<SoundGameTrack> _soundPlayer;
        private GamePlayViewModel _gamePlayViewModel;

        private readonly List<Func<GamePlayViewModel, Task>> _subscribers = new();

        public void Subscribe(Func<GamePlayViewModel, Task> handler)
        {
            _subscribers.Add(handler);
        }

        private async Task NotifySubscribers(GamePlayViewModel viewModel)
        {
            if (_subscribers.Count == 0) return;
            var tasks = _subscribers.Select(subscriber => subscriber(viewModel));
            await Task.WhenAll(tasks);
        }

        public GameController
            (
                IGameService gameService, 
                ISoundPlayer<SoundGameTrack> soundPlayer, 
                ISettingsProvider settingsProvider
            )
        {
            _gameService = gameService;
            _soundPlayer = soundPlayer;
            _gameSettings = settingsProvider.GameSettings;
            _soundSettings = settingsProvider.SoundSettings;
            _gamePlayViewModel = new GamePlayViewModel();
        }

        public void StartGame()
        {
            Console.WriteLine("Игра началась!");
            _soundPlayer.Play(SoundGameTrack.GamePlay);
            _gameService.StartGame();
            EnterNumber().Wait();
        }

        public void ExitGame()
        {
            Console.WriteLine("Выход из игры.");
            Environment.Exit(0);
        }

        public void RestartGame()
        {
            _gameService.ResetGame();
        }

        public void ChangeAttempts(int delta)
        {
            _gameSettings.MaxAttemps = Math.Clamp(_gameSettings.MaxAttemps + delta, 1, 100);
        }

        public void ChangeVolume(int delta)
        {
            _soundSettings.SoundVolume = Math.Clamp(_soundSettings.SoundVolume + delta, 0, 100);
        }

        public void ToggleMute()
        {
            _soundSettings.SoundMute = !_soundSettings.SoundMute;
        }

        public void ChangeMinValue(int delta)
        {
            _gameSettings.MinValue = Math.Clamp(_gameSettings.MinValue + delta, 0, 100);
        }

        public void ChangeMaxValue(int delta)
        {
            _gameSettings.MaxValue = Math.Clamp(_gameSettings.MaxValue + delta, 0, 100);
        }

        private async Task GuessNumber(int guess)
        {
            GuessResult result = _gameService.MakeGuess(guess);

            switch (result)
            {
                case GuessResult.TooLow:
                    _gamePlayViewModel.GameStatus = GuessResult.TooLow;
                    break;
                case GuessResult.TooHigh:
                    _gamePlayViewModel.GameStatus = GuessResult.TooHigh;
                    break;
                case GuessResult.Correct:
                    _gamePlayViewModel.GameStatus = GuessResult.Correct;
                    break;
                case GuessResult.GameOver:
                    _gamePlayViewModel.TargetNumber = _gameService.GetTargetNumber();
                    _gamePlayViewModel.GameStatus = GuessResult.GameOver;
                    break;
            }

            _gamePlayViewModel.Attempts = _gameService.GetAttempt();

            await OnGameStateChanged();

            if (result != GuessResult.GameOver) await EnterNumber();
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

        protected virtual async Task OnGameStateChanged()
        {
            await NotifySubscribers(_gamePlayViewModel);
        }
    }
}
