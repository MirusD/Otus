namespace Librarian.Infrastructure
{
    internal class UIConsole
    {
        public delegate void Notify(ConsoleKeyInfo key);
        public event Notify? OnPressKey;

        public void PrintMenu()
        {
            Console.WriteLine("" +
                "--------------- Меню -----------------\n" +
                "1 - Добавить книгу\n" +
                "2 - Вывести список не прочитанного\n" +
                "3 - Выйти\n" +
                "--------------------------------------");

            WaitingKeyToPress();
        }

        public void WaitingKeyToPress()
        {
            var key = Console.ReadKey(true);
            OnPressKey?.Invoke(key);
        }

    }
}
