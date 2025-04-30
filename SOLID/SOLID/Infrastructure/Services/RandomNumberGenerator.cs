using SOLID.Domain.Interfaces;

namespace SOLID.Infrastructure.Services
{
    class RandomNumberGenerator : INumberGenerator
    {
        private readonly Random _random = new Random();

        public int Generate(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }
    }
}
