
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;

namespace SOLID.Infrastructure.Settings.Interfaces
{
    interface ISettingsProvider
    {
        public IGameSettings GameSettings { get; }
        public ISoundSettings SoundSettings { get; }
    }
}
