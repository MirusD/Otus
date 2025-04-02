using SOLID.BLL.Domain.Settings.Interfaces;

namespace SOLID.BLL.Services.Abstractions
{
    public interface IGameService
    {
        /// <summary>
        /// Метод возвращает объект с настройками
        /// </summary>
        /// <returns>Объект с настройками</returns>
        public IGameSettings GetSettings();

        /// <summary>
        /// Метод для установки настроек игры
        /// </summary>
        /// <param name="gameSettings">Объект с настройками</param>
        public void SetSettings(IGameSettings gameSettings);

        /// <summary>
        /// Начало игры
        /// </summary>
        public void Start();

        /// <summary>
        /// Метод для отгадываемого числа
        /// </summary>
        /// <param name="number">Отгадываемое число</param>
        /// <returns>-1 меньше, 1 больше, 0 равно</returns>
        public int? GuessNumber(int number);

        /// <summary>
        /// Метод для получения оставшихся попыток
        /// </summary>
        /// <returns>Сколько попыток осталось</returns>
        public int GetAttemptCount();

        /// <summary>
        /// Метод возвращает загаданное программой число
        /// </summary>
        /// <returns>Отгадываемое число</returns>
        public int GetAnswer();
    }
}
