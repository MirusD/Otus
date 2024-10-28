namespace LINQoperators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sortedListByPercentage = list.Top(30);

            string res = String.Join(",", sortedListByPercentage);

            Console.WriteLine(res);

            var persons = new List<Person>
            {
               new Person { Name = "Петрович", Age = 55 },
               new Person { Name = "Иваныч", Age = 68 },
               new Person { Name = "Михалыч", Age = 40 },
               new Person { Name = "Алексеич", Age = 20 },
               new Person { Name = "Махмутыч", Age = 18 },
               new Person { Name = "Николаыч", Age = 28 },
               new Person { Name = "Пупкиныч", Age = 38 },
               new Person { Name = "Василич", Age = 32 },
               new Person { Name = "Александрыч", Age = 45 }
            };

            var sortPersons = persons.Top(30, person => person.Age);

            Console.WriteLine("-----------------------------------");
            foreach (var person in sortPersons)
            {
                Console.WriteLine($"{person.Name} : {person.Age}");
            }

            Console.ReadLine();
        }
    }
}