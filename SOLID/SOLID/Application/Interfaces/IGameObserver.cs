using SOLID.Domain.Interfaces;

namespace SOLID.Application.Interfaces
{
    interface IGameObserver
    {
        void OnGameStateChanged(IGameState state);
    }
}
