using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;

namespace SOLID.Domain.State
{
    class GameState : IGameState
    {
        public GameStatus GameStatus { get; set; }

        public GuessResult? GuessResult { get; set; } = null;

        int IGameState.Attempts { get; set; }

        int IGameState.TargetNumber { get; set; }
    }
}
