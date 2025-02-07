using System.Diagnostics;

namespace Parallelism
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Старт программы");

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(basePath, @"files");

            await CalculateNumberOfSpacesAsync(path);
        }

        static async Task CalculateNumberOfSpacesAsync(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files;
                files = Directory.GetFiles(path);

                FileReader reader = new FileReader();
                reader.OnStartReading += message => Console.WriteLine($"[Уведомление:] {message}");
                reader.Error += message => Console.WriteLine($"[Ошибка:] {message}");

                try
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    var tasks = files.Select(async filePath =>
                    {
                        try
                        {
                            string content = await reader.ReadFileAsync(filePath);
                            int spaceCount = content.Count(c => c == ' ');
                            Console.WriteLine($"Файл: {Path.GetFileName(filePath)} содержит {spaceCount} пробелов");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при обработке файла {Path.GetFileName(filePath)}: {ex.Message}");
                        }
                    });

                    await Task.WhenAll(tasks);

                    stopwatch.Stop();
                    Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} миллисекунд");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Указанная директория не существует.");
            }
        }
    }
}
