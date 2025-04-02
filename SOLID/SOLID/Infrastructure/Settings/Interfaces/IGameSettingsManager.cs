using SOLID.BLL.Domain.Settings;
using SOLID.BLL.Domain.Settings.Interfaces;

namespace SOLID.Infrastructure.Settings.Interfaces
{
    interface IGameSettingsManager
    {
        public T LoadSettings<T>() where T : GameSettings, new();

        public void SaveSettings<T>(T settings) where T : IGameSettings;
    }
}
