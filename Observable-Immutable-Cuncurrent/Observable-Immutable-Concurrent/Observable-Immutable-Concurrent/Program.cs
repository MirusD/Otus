namespace Observable_Immutable_Concurrent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var shop = new Shop();
            var customer = new Customer("Боб");
            shop.SubscribeToChangeProductCollection(customer.OnItemChanged);

            Console.WriteLine("" +
                "A - Добавить товар\n" +
                "D - Удалить товар\n" +
                "X - Выход из программы");

            while (true) 
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.A:
                        shop.Add($"Товар {DateTime.Now}");
                        break;
                    case ConsoleKey.D:
                        Console.Write("Какой товар надо удалить?:");
                        int.TryParse(Console.ReadLine(), out int id);
                        shop.Remove(id);
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("Выход из программы");
                        break;
                    default:
                        Console.WriteLine("Нет такой комманды");
                        break;
                }

                if (key.Key == ConsoleKey.X) break;
            }
        }
    }
}