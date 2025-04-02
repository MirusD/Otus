using SOLID.BLL.Domain.Settings;
using SOLID.BLL.Domain.Settings.Interfaces;
using SOLID.Infrastructure.Settings.Interfaces;
using System.Text.Json;

namespace SOLID.Infrastructure.Settings
{
    class GameSettingsManager : IGameSettingsManager
    {
        private static readonly string SettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gameSettings.json");

        public T LoadSettings<T>() where T : GameSettings, new()
        {
            if (!File.Exists(SettingsFile))
            {
                var defaultSettings = new T();
                SaveSettings(defaultSettings);
                return defaultSettings;
            }

            string json = File.ReadAllText(SettingsFile);
            return JsonSerializer.Deserialize<T>(json) ?? new T();
        }

        public void SaveSettings<T>(T settings) where T : IGameSettings
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFile, json);
        }
    }
}
