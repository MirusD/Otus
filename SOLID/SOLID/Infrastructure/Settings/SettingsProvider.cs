using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Infrastructure.Settings.Interfaces;

namespace SOLID.Infrastructure.Settings
{
    class SettingsProvider : ISettingsProvider
    {
        private readonly IGameSettings _gameSettings;
        private readonly ISoundSettings _soundSettings;

        public SettingsProvider(IGameSettings gameSettings, ISoundSettings soundSettings)
        {
            _gameSettings = gameSettings;
            _soundSettings = soundSettings;
        }

        public IGameSettings GameSettings { get { return _gameSettings; } }

        public ISoundSettings SoundSettings { get { return _soundSettings; } }
    }
}
