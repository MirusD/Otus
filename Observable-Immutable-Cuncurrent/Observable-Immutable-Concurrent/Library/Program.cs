using Librarian.Domain;
using Librarian.Infrastructure;
using System.Collections.Concurrent;

namespace Library
{
    internal class Program
    {
        private static ConcurrentDictionary<string, int> books = new ConcurrentDictionary<string, int>();
        static async Task Main(string[] args)
        {
            var UI = new UIConsole();
            var reader = new Reader("Михаил", books);

            Task.Run(async () => ReadBooks());

            UI.OnPressKey += (key) =>
            {
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Write("Введите название книги: ");
                        string bookName = Console.ReadLine() ?? string.Empty;
                        reader.Add(bookName);
                        UI.PrintMenu();
                        break;
                    case ConsoleKey.D2:
                        ConcurrentDictionary<string, int> books = reader.GetListBooks();
                        Console.WriteLine("============= Книги ==============");
                        if (books.IsEmpty)
                            Console.WriteLine($"{reader.Name} у вас нет книг для чтения");
                        foreach (var book in books)
                        {
                            Console.WriteLine($"{book.Key} - {book.Value}%");
                        }
                        Console.WriteLine("==================================");
                        UI.PrintMenu();
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("Выход из программы");
                        break;
                    default:
                        Console.WriteLine("Нет такой комманды");
                        UI.PrintMenu();
                        break;
                }
            };

            UI.PrintMenu();
        }

        static async Task ReadBooks()
        {
            while (true)
            {
                await Task.Delay(1000);

                foreach (var book in books)
                {
                    if (book.Value < 100)
                    {
                        books.AddOrUpdate(book.Key, 0, (k, oldValue) => oldValue + 1);
                    }
                    else
                    {
                        books.TryRemove(book.Key, out int removeValue);
                    }
                }
            }
        }
    }
}