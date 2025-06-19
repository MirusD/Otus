using SOLID.Presentation.ConsoleApp;

namespace SOLID.Presentation
{
    class PresentationRoot
    {
        private readonly AppConsole _consoleRoot;

        public PresentationRoot(AppConsole consoleApp)
        {
            _consoleRoot = consoleApp;
        }

        public void StartConsoleUI()
        {
            _consoleRoot.Init();
        }
    }
}
