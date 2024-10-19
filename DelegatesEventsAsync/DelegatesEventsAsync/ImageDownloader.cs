namespace DelegatesEventsAsync
{
    /// <summary>
    /// Класс организующий методы скачивания файлов
    /// </summary>
    internal class ImageDownloader
    {
        public delegate void Notify(string fileName);
        public event Notify? Started;
        public event Notify? Completed;
        public event Notify? Cancelling;
        public event Notify? Exception;

        /// <summary>
        /// Метод для скачивания файлов
        /// </summary>
        /// <param name="url">Ссылка для скачивания</param>
        /// <param name="fileName">Имя файла которое будет присвоено скаченному файлу</param>
        /// <param name="downloadDirectory">В какую директорию скачивать файлы</param>
        /// <param name="token">Токен для отмены</param>
        /// <returns>Task</returns>
        async public Task Downdoad(string url, string fileName, string downloadDirectory, CancellationToken token)
        {

            using (var myWebClient = new HttpClient())
            {
                try
                {
                    Started?.Invoke(fileName);
                    await Task.Delay(5000);

                    HttpResponseMessage response = await myWebClient.GetAsync(url, token);
                    response.EnsureSuccessStatusCode();
  
                    // WebClient не потдерживает токен отмены и является устаревшим
                    //myWebClient.DownloadFileTaskAsync(remoteUri, fileName).Wait(token);

                    string filePath = Path.Combine(downloadDirectory, fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs, token);
                        Completed?.Invoke(fileName);
                    }

                }
                catch (OperationCanceledException)
                {
                    Cancelling?.Invoke(fileName);
                    throw;
                }
                catch (Exception ex)
                {
                    Exception?.Invoke(fileName);
                }
            }
        }
    }
}
