using SOLID.Presentation.ConsoleApp.Menu.Items;

namespace SOLID.Presentation.ConsoleApp.Menu
{
    class Menu
    {
        private readonly string _title;
        private readonly MenuItem[] _items;

        public Menu(string title, MenuItem[] items)
        {
            _title = title;
            _items = items;
        }

        public void Show()
        {
            int index = 0;

            while (true)
            {
                Console.Clear();
                DrawTitle();
                DrawMenu(index);

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + _items.Length) % _items.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % _items.Length;
                        break;
                    case ConsoleKey.LeftArrow:
                        _items[index].OnLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        _items[index].OnRight();
                        break;
                    case ConsoleKey.Enter:
                        _items[index].OnSelect();
                        break;
                }
            }
        }

        private void DrawTitle()
        {
            Console.WriteLine(_title);
            Console.WriteLine(new string('-', _title.Length));
        }

        private void DrawMenu(int selectedIndex)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (i == selectedIndex)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ResetColor();

                Console.WriteLine($"{(i == selectedIndex ? ">" : " ")} {_items[i].GetDisplayText()}");
            }
            Console.ResetColor();
        }
    }
}
