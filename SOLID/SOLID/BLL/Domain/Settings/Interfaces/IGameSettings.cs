namespace SOLID.BLL.Domain.Settings.Interfaces
{
    public interface IGameSettings
    {
        public int Attempts { get; set; }
        public RangeNumbers Range { get; set; }
        public int MusicVolume { get; set; }
        public bool MusicMute { get; set; }
    }
}
