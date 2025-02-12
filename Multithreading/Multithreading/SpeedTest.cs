using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Multithreading
{
    internal class SpeedTest
    {
        private int[] ArrSizes = [100000, 1000000, 10000000];
        public int NumberOfCycles = 0;
        private Dictionary<string, List<long>> Results = new Dictionary<string, List<long>>();

        public SpeedTest(int numberCycles)
        {
            NumberOfCycles = numberCycles + 1;
        }


        public void Run(Func<int[], long> func)
        {
            var times = new List<long>();
            var sw = new Stopwatch();

            foreach (int size in ArrSizes)
            {
                int[] data = Enumerable.Range(1, size).ToArray();

                for (int i = 0; i < NumberOfCycles; i++)
                {
                    sw.Restart();
                    sw.Start();
                    func(data);
                    sw.Stop();

                    long time = sw.ElapsedMilliseconds;
                    times.Add(time);
                }
            }

            Results.Add(func.Method.Name, times);
        }

        public void PrintPCSpecifications()
        {
            Console.WriteLine("=== Информация о системе ===");
            Console.WriteLine($"ОС: {RuntimeInformation.OSDescription}");
            Console.WriteLine($"Архитектура процессора: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"Процессор: {Environment.ProcessorCount} ядер");
            Console.WriteLine($"Оперативная память: {GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / (1024 * 1024 * 1024)} ГБ");
            Console.WriteLine($"Версия .NET: {RuntimeInformation.FrameworkDescription}");
        }

        public void PrintResults()
        {
            PrintPCSpecifications();
            int width = 65;

            foreach (var data in Results)
            {
                int textWidth = data.Key.Length;
                int paddingTotal = width - 2 - textWidth;
                int paddingLeft = paddingTotal / 2;
                int paddingRight = paddingTotal - paddingLeft;

                Console.WriteLine($"+{new string('-', width - 2)}+");
                Console.WriteLine($"|{new string(' ', paddingLeft)}{data.Key}{new string(' ', paddingRight)}|");
                Console.WriteLine($"+{new string('-', width - 2)}+");

                var list = data.Value;

                for (int a = 0; a < NumberOfCycles; a++)
                {
                    if (a == 0)
                    {
                        Console.Write($"{("Цыклы ->"),-9}");
                    }
                    else
                    {
                        Console.Write($"| {a,-9}");
                    }
                }
                Console.WriteLine("|");
                Console.WriteLine($"+{new string('-', width - 2)}+");

                int count = 0;

                for (int i = 0; i < ArrSizes.Length; i++)
                {
                    Console.Write($"{ArrSizes[i], -9}");
                    for (var b = count; b < count + NumberOfCycles - 1; b++)
                    {
                        Console.Write($"| {list[b], -9}");
                    }

                    Console.WriteLine("|");
                    count += NumberOfCycles;
                }
            }

            Console.WriteLine($"+{new string('-', width - 2)}+");
        }
    }
}
