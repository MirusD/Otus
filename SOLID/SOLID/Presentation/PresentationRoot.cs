using SOLID.Presentation.ConsoleApp;

namespace SOLID.Presentation
{
    class PresentationRoot
    {
        private readonly ConsoleMain _consoleRoot;

        public PresentationRoot(ConsoleMain consoleApp)
        {
            _consoleRoot = consoleApp;
        }

        public void StartConsoleUI()
        {
            _consoleRoot.Start();
        }
    }
}
