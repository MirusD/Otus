using System.Collections.Concurrent;

namespace Librarian.Domain
{
    internal class Reader
    {
        private ConcurrentDictionary<string, int> _books;
        public string Name { get; }

        public Reader(string name, ConcurrentDictionary<string, int> books)
        {
            _books = books;
            Name = name;
        }

        public void Add(string bookName)
        {
            bool isAdded = _books.TryAdd(bookName, 0);
        }

        public ConcurrentDictionary<string, int> GetListBooks()
        {
            return _books;
        }
    }
}
