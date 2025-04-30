using SOLID.Domain.Enums;

namespace SOLID.Application.Interfaces
{
    interface IGameService
    {
        void StartGame();

        GuessResult MakeGuess(int guess);

        void ResetGame();

        int GetAttempt();

        int GetTargetNumber();
    }
}
