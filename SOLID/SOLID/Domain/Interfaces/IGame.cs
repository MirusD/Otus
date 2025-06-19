using SOLID.Domain.Enums;

namespace SOLID.Domain.Interfaces
{
    interface IGame
    {
        void ResetGame();

        GuessResult? MakeGuess(int guess);

        IGameState GetState();
    }
}
