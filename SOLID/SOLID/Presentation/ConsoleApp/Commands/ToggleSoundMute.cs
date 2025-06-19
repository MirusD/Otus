using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class ToggleSoundMute : ICommand
    {
        private ISoundSettings _soundSettings;
        public ToggleSoundMute(ISoundSettings soundSettings)
        {
            _soundSettings = soundSettings;
        }
        public void Execute()
        {
            _soundSettings.SoundMute = !_soundSettings.SoundMute;
        }
    }
}
