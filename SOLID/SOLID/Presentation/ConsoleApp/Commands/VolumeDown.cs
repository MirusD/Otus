using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Commands
{
    class VolumeDown : ICommand
    {
        private ISoundSettings _soundSettings;
        public VolumeDown(ISoundSettings soundSettings) 
        {
            _soundSettings = soundSettings;
        }
        public void Execute()
        {
            _soundSettings.SoundVolume = Math.Clamp(_soundSettings.SoundVolume - 1, 0, 100);
        }
    }
}
