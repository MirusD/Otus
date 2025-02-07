namespace Parallelism
{
    internal class FileReader
    {
        public event Action<string>? OnStartReading;
        public event Action<string>? Error;

        public async Task<string> ReadFileAsync(string filePath)
        {
            try
            {
                OnStartReading?.Invoke($"Начало чтения файла: {Path.GetFileName(filePath)}");
                using StreamReader reader = new StreamReader(filePath);
                return await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                Error?.Invoke($"Ошибка при чтении файлв {Path.GetFileName(filePath)}: {ex.Message}");
                throw;
            }
        }
    }
}
