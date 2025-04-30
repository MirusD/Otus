using SOLID.Domain.Entitites;
using SOLID.Domain.Interfaces;

namespace SOLID.Infrastructure.Factories
{
    class GameFactory : IGameFactory
    {
        private readonly INumberGenerator _numberGenerator;

        public GameFactory(INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
        }

        public IGame CreateGame(IGameSettings gameSettings)
        {
            return new GuessNumberGame(_numberGenerator, gameSettings);
        }
    }
}
