using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp.Events
{
    class GameStateChangedEventArgs : EventArgs
    {
        public GamePlayViewModel GamePlayViewModel { get; }

        public GameStateChangedEventArgs(GamePlayViewModel gamePlayViewModel)
        {
            GamePlayViewModel = gamePlayViewModel;
        }
    }
}
