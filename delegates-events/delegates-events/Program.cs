var searcher = new FileSearcher();
var foundFiles = new List<string>();
var cts = new CancellationTokenSource();

searcher.FileFound += (sender, e) =>
{
    Console.WriteLine($"Файл найден: {e.FileName}");
    foundFiles.Add(e.FileName);
};

string pathToSearch = Path.Combine(AppContext.BaseDirectory, "Files");
searcher.Search(pathToSearch, cts.Token);

var maxFile = foundFiles.GetMax(f => new FileInfo(f).Length);
Console.WriteLine();
Console.WriteLine(maxFile != null
    ? $"Файл с максимальным размером: {maxFile} ({new FileInfo(maxFile).Length} байт)"
    : "Файлы не найдены.");
