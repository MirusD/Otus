using System.Collections.Specialized;

namespace Observable_Immutable_Concurrent
{
    /// <summary>
    /// Реализует объект покупатель
    /// </summary>
    internal class Customer
    {
        public string Name { get; }
        public Customer(string name) 
        {
            Name = name;
        }

        public void OnItemChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Item newItem in e.NewItems)
                    {
                        Console.WriteLine($"" +
                            $"{Name} в магазине добавлен новый товар:\n" +
                            $"Name: {newItem.Name}\n" +
                            $"Id: {newItem.Id}");
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Item oldItem in e.OldItems)
                    {
                        Console.WriteLine($"" +
                            $"{Name} в магазине удалён товар:\n" +
                            $"Name: {oldItem.Name}\n" +
                            $"Id: {oldItem.Id}");
                    }
                    break;
            }
        }
    }
}
