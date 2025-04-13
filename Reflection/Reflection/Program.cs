using System.Diagnostics;
using System.Text.Json;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var f = F.Get();

            var (csv, csvSerializeTime) = BenchmarkTime.Measure(() => CsvSerialize(f));
            Console.WriteLine($"Время выполнения CSV сериализации: {csvSerializeTime.ElapsedMilliseconds} мс.");

            Console.WriteLine(new string('-', 50));

            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Результат CSV сериализации: {csv}");
            sw.Stop();
            Console.WriteLine($"Время вывода текста CSV в консоль: {sw.ElapsedMilliseconds} мс.");

            Console.WriteLine(new string('-', 50));

            var (fObj, csvDeserializeTime) = BenchmarkTime.Measure<F>(() => CsvDeserialize<F>(csv));
            Console.WriteLine($"Результат CSV десериализации: {fObj.GetType()}");
            Console.WriteLine($"Время CSV десериализации: {csvDeserializeTime.ElapsedMilliseconds} мс.");

            Console.WriteLine(new string('-', 50));

            var (json, jsonSerializeTime) = BenchmarkTime.Measure(() => JsonSerialize(f));
            Console.WriteLine($"Результат JSON сериализации: {json}");
            Console.WriteLine($"Время JSON сериализацию: {jsonSerializeTime.ElapsedMilliseconds} мс.");
        }

        static string CsvSerialize<T>(T f) where T : new()
        {
            int iterations = 100000;
            string result = "";

            for (int i = 0; i < iterations; i++)
            {
                result = CsvSerializer<T>.Serialize(f);
            }

            return result;
        }

        static T CsvDeserialize<T>(string csv) where T : new()
        {
            return CsvSerializer<T>.Deserialize(csv);
        }

        static string JsonSerialize<T>(T f)
        {
            return JsonSerializer.Serialize(f);
        }
    }
}
