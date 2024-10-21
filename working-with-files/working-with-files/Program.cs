using System.IO;
using System.Text;

namespace working_with_files
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string dir1 = @"c:\Otus\TestDir1";
            string dir2 = @"c:\Otus\TestDir2";

            try
            {
                CreateDirectory(dir1);
                CreateDirectory(dir2);

                List<Task> tasks = new List<Task>();

                for (int i = 0; i < 10; i++) 
                {
                    string fileName = $"File{i}.txt";
                    string content = $"{fileName}\n{DateTime.Now}";

                    tasks.Add(Task.Run(async () => await CreateFileAndWriteTextAsync(dir1, fileName, content)));
                    tasks.Add(Task.Run(async () => await CreateFileAndWriteTextAsync(dir2, fileName, content)));
                }

                await Task.WhenAll(tasks);
                tasks.Clear();

                Console.WriteLine("\nЧтение файлов");
                Console.WriteLine(new string('-', 50));

                for (int i = 0; i < 10; i++)
                {
                    string fileName = $"File{i}.txt";
                    tasks.Add(Task.Run(async () => await ReadFileFromDirectoryAsync(dir1, fileName)));
                    tasks.Add(Task.Run(async () => await ReadFileFromDirectoryAsync(dir2, fileName)));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }

            static void CreateDirectory(string path)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                    Console.WriteLine($"Директория {path} создана.");
                }
                else
                {
                    Console.WriteLine($"Директория {path} уже существует.");
                }
            }

            static async Task CreateFileAndWriteTextAsync(string directoryPaht, string fileName, string content)
            {
                string filePath = Path.Combine(directoryPaht, fileName);

                try
                {
                    await File.WriteAllTextAsync(filePath, content);
                    Console.WriteLine($"Запись в файл: {filePath} произведена!");
                }
                catch (UnauthorizedAccessException) 
                {
                    Console.WriteLine($"Ошибка: нет прав на запись в файл {filePath}");
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"Ошибка при работе с файлом {filePath}: {ioEx.Message}");
                }
            }

            static async Task ReadFileFromDirectoryAsync(string dirPath, string fileName)
            {
                string filePath = Path.Combine(dirPath, fileName);

                try
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        var fileTextLine = new List<string>();
                        string? line;

                        while ((line = await reader.ReadLineAsync()) != null) 
                        {
                            fileTextLine.Add(line);
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append($"{fileName}: ");

                        for (var i = 0; i < fileTextLine.Count; i++)
                        {
                            sb.Append(fileTextLine[i]);
                            if (i < fileTextLine.Count - 1)
                            {
                                sb.Append(" + ");
                            }
                        }

                        Console.WriteLine(sb);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Ошибка: нет прав на чтение из файла {filePath}");
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"Ошибка при работе с файлом {filePath}: {ioEx.Message}");
                }

            }
        }
    }
}