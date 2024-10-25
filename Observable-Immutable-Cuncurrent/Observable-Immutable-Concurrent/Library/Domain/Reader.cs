using System.Collections.Concurrent;

namespace Librarian.Domain
{
    internal class Reader
    {
        private ConcurrentDictionary<string, int> Books;
        public string Name;

        public Reader(string name, ConcurrentDictionary<string, int> books)
        {
            Books = books;
            Name = name;
        }

        public void Add(string bookName)
        {
            bool isAdded = Books.TryAdd(bookName, 0);
        }

        public ConcurrentDictionary<string, int> GetListBooks()
        {
            return Books;
        }
    }
}
