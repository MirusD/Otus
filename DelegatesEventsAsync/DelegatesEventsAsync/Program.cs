namespace DelegatesEventsAsync
{
    internal class Program
    {
        static Dictionary<string, string> downloadStatus = new Dictionary<string, string>();

        static Dictionary<string, string> downloadFiles = new Dictionary<string, string>()
        {
            { "1.jpg", "https://images.wallpaperscraft.ru/image/single/kordilery_gory_zakat_48438_3840x2400ww.jpg" },
            { "2.jpg", "https://images.wallpaperscraft.ru/image/single/sova_dikaia_ptitsa_ptitsa_1323373_1920x1080.jpg"},
            { "3.jpg", "https://images.wallpaperscraft.ru/image/single/devushka_kot_anime_1323430_1920x1080.jpg"},
            { "4.jpg", "https://images.wallpaperscraft.ru/image/single/derevo_lug_pole_1323348_1920x1080.jpg"},
            { "5.jpg", "https://images.wallpaperscraft.ru/image/single/gekkon_iashcheritsa_dikaia_priroda_1322478_1920x1080.jpg" },
            { "6.jpg", "https://images.wallpaperscraft.ru/image/single/gory_dolina_tuman_1322400_1920x1080.jpg" },
            { "7.jpg", "https://images.wallpaperscraft.ru/image/single/orel_ptitsa_dikaia_priroda_1321772_1920x1080.jpg" },
            { "8.jpg", "https://images.wallpaperscraft.ru/image/single/lampochki_svet_razmytie_1321751_1920x1080.jpg"},
            { "9.jpg", "https://images.wallpaperscraft.ru/image/single/most_derevia_vetki_1321769_1920x1080.jpg" },
            { "10.jpg","https://images.wallpaperscraft.ru/image/single/lampochki_svet_razmytie_1321751_1920x1080.jpg" }
        };

        static async Task Main(string[] args)
        {
            var ImageDownloader = new ImageDownloader();
            ImageDownloader.Started += (fileName) =>
            {
                Console.WriteLine($"Скачивание файла '{fileName}' началось");
                downloadStatus[fileName] = "Загрузка...";
            };
            ImageDownloader.Completed += (fileName) =>
            {
                Console.WriteLine($"Скачивание файла '{fileName}' закончилось");
                downloadStatus[fileName] = "Загружен";
            };
            ImageDownloader.Cancelling += (fileName) =>
            {
                Console.WriteLine($"Скачивание файла '{fileName}' отменено");
                downloadStatus[fileName] = "Отменено";
            };
            ImageDownloader.Exception += (fileName) =>
            {
                Console.WriteLine($"Ошибка загрузки файла: '{fileName}'");
                downloadStatus[fileName] = "Ошибка загрузки";
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task> downloadTasks = new List<Task>();

            Console.WriteLine(
                "Нажмите следующие клавиши:" +
                "\nS - для начала скачивания файлов " +
                "\nA - для остановки скачивании" +
                "\nE - для выхода" +
                "\n* - любую другую клавишу для проверки статуса скачивания\n");

            Task keyboardTask = Task.Run(() =>
            {
                while(true)
                {
                    string downloadDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.S)
                    {
                        foreach(var file in downloadFiles)
                        {
                            downloadStatus[file.Key] = "Подготовка...";

                            downloadTasks.Add(ImageDownloader.Downdoad(file.Value, file.Key, downloadDirectory, cts.Token));
                        }
                    }
                    else if (key.Key == ConsoleKey.A)
                    {
                        Console.WriteLine("Отмена всех загрузок...");
                        cts.Cancel();
                    }
                    else if (key.Key == ConsoleKey.E)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("======================");
                        Console.WriteLine("Состояние загрузок");
                        Console.WriteLine("======================");
                        foreach (var status in downloadStatus)
                        {
                            Console.WriteLine($"{status.Key}: {status.Value}");
                        }
                    }
                }
            });

            try
            {
                await Task.WhenAll(downloadTasks);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Загрузки были отменены");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await keyboardTask;
            Console.WriteLine("Программа завершена");
        }
    }   
}