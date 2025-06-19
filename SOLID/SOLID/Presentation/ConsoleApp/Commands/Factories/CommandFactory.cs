using SOLID.Application.Interfaces;
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Infrastructure.Settings.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;
using SOLID.Presentation.ConsoleApp.Controller;
using SOLID.Presentation.ConsoleApp.Enums;

namespace SOLID.Presentation.ConsoleApp.Commands.Factories
{
    class CommandFactory : ICommandFactory
    {
        private readonly IGameService _gameService;
        private readonly IGameSettings _gameSettings;
        private readonly ISoundSettings _soundSettings;
        private readonly ISoundPlayer<SoundGameTrack> _soundPlayer;
        private readonly GameController _gameController;

        public CommandFactory
            (
                IGameService gameService,
                ISoundPlayer<SoundGameTrack> soundPlayer,
                ISettingsProvider settingsProvider,
                GameController gameController
        ) 
        {
            _gameService = gameService;
            _soundPlayer = soundPlayer;
            _gameSettings = settingsProvider.GameSettings;
            _soundSettings = settingsProvider.SoundSettings;
            _gameController = gameController;
        }

        public ICommand Create(CommandType type)
        {
            return type switch
            {
                CommandType.StartGame => new StartGame(_gameService, _gameController),
                CommandType.ExitGame => new ExitGame(),
                CommandType.VolumeUp => new VolumeUp(_soundSettings),
                CommandType.VolumeDown => new VolumeDown(_soundSettings),
                CommandType.ToggleSoundMute => new ToggleSoundMute(_soundSettings),
                CommandType.AttemptsUp => new AttemptsUp(_gameSettings),
                CommandType.AttemptsDown => new AttemptsDown(_gameSettings),
                CommandType.MinValueUp => new MinValueUp(_gameSettings),
                CommandType.MinValueDown => new MinValueDown(_gameSettings),
                CommandType.MaxValueUp => new MaxValueUp(_gameSettings),
                CommandType.MaxValueDown => new MaxValueDown(_gameSettings),
                _ => throw new ArgumentException($"Неизвестная команда: {type}")
            };
        }
    }
}
