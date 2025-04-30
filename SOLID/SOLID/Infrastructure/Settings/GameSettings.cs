using SOLID.Domain.Interfaces;

namespace SOLID.Infrastructure.Settings
{
    class GameSettings : IGameSettings
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int MaxAttemps { get; set; }

        public GameSettings(int minValue = 0, int maxValue = 100, int maxAttemps = 10)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            MaxAttemps = maxAttemps;
        }
    }
}
