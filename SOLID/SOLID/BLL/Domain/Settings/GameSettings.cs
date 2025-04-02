using SOLID.BLL.Domain;
using SOLID.BLL.Domain.Settings.Interfaces;

namespace SOLID.BLL.Domain.Settings
{
    class GameSettings : IGameSettings
    {
        public int Attempts { get; set; } = 10;
        public RangeNumbers Range { get; set; } = new RangeNumbers();

        public int MusicVolume { get; set; } = 30;

        public bool MusicMute { get; set; } = false;

        public GameSettings() { }
    }
}
