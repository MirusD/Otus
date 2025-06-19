using SOLID.Domain.Enums;
namespace SOLID.Domain.Interfaces
{
    interface IGameState
    {
        GameStatus GameStatus { get; set; }

        GuessResult? GuessResult { get; set; }

        int Attempts { get; set; }

        int TargetNumber { get; set; }
    }
}
