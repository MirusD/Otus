using SOLID.Domain.Enums;

namespace SOLID.Presentation.ConsoleApp.ViewModels
{
    class GamePlayViewModel
    {
        public int Attempts { get; set; }

        public int? TargetNumber { get; set; }

        public string LastMessage { get; set; }

        public GuessResult GameStatus { get; set; }

        public GamePlayViewModel()
        {
            Attempts = 0;
            TargetNumber = null;
            LastMessage = string.Empty;
            GameStatus = GuessResult.GameOver;
        }
    }
}
