namespace SOLID.Presentation.ConsoleApp
{
    class ConsoleMain
    {
        private readonly ConsoleSound _consoleSound;
        private readonly ConsoleUI _consoleUI;

        public ConsoleMain(ConsoleUI consoleUI, ConsoleSound consoleSound)
        {
            _consoleUI = consoleUI;
            _consoleSound = consoleSound;
        }

        public void Start()
        {
            _consoleSound.Start();
            _consoleUI.Start();
        }
    }
}
