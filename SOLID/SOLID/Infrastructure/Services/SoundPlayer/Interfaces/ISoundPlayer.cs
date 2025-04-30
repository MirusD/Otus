namespace SOLID.Infrastructure.Services.SoundPlayer.Interfaces
{
    public interface ISoundPlayer<T>
        where T : Enum
    {
        void SetPlayList(Dictionary<T, string> playList);
        Task Play(T soundKey);
        void StopMusic();
        void SetVolume(int volume);
    }
}
