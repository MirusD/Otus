using SOLID.Presentation.ConsoleApp.Events;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp.Interfaces
{
    interface IGameController
    {
        public void Subscribe(Func<GamePlayViewModel, Task> handler);

        public void StartGame();

        public void RestartGame();

        public void ExitGame();

        public void ChangeVolume(int delta);

        public void ChangeAttempts(int delta);

        public void ToggleMute();

        public void ChangeMinValue(int delta);

        public void ChangeMaxValue(int delta);
    }
}
