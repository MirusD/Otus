namespace SOLID.Infrastructure.Services.SoundPlayer.Settings
{
    class SoundSettings : ISoundSettings
    {
        public int SoundVolume { get; set; } = 30;
        public bool SoundMute { get; set; } = false;
    }
}
