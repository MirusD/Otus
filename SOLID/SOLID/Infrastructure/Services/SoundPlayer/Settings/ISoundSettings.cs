namespace SOLID.Infrastructure.Services.SoundPlayer.Settings
{
    public interface ISoundSettings
    {
        int SoundVolume { get; set; }
        bool SoundMute { get; set; }
    }
}
