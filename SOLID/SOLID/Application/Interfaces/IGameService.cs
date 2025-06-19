using SOLID.Domain.Enums;

namespace SOLID.Application.Interfaces
{
    interface IGameService
    {
        void StartGame();

        void MakeGuess(int guess);

        void ResetGame();

        void Subscribe(IGameObserver observer);

        void Unsubscribe(IGameObserver observer);
    }
}
