public class FileSearcher
{
    public event EventHandler<FileArgs> FileFound;

    public void Search(string directory, CancellationToken cancellationToken)
    {
        foreach (var file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Поиск отменён через токен отмены.");
                break;
            }

            FileFound?.Invoke(this, new FileArgs(file));
        }
    }
}
