using SOLID.BLL.Domain.Settings;
using SOLID.BLL.Domain.Settings.Interfaces;
using SOLID.BLL.Services.Abstractions;
using SOLID.Infrastructure.MusicPlayer.Enums;
using SOLID.Infrastructure.MusicPlayer.Interfaces;
using SOLID.Infrastructure.Settings.Interfaces;

namespace SOLID.Infrastructure
{
    class MenuItem
    {
        public string Name { get;  }
        public object? Value { get; set; }
        public Action? OnLeft { get; set; }
        public Action? OnRight { get; set; }
        public Action? OnEnter { get; set; }

        public MenuItem(string name, object? value = null, Action? onLeft = null, Action? onRight = null, Action? onEnter = null)
        {
            Name = name;
            Value = value;
            OnLeft = onLeft;
            OnRight = onRight;
            OnEnter = onEnter;
        }
    }

    class GameUI
    {
        private IGameService _gameService;
        private IMusicPlayer _musicPlayer;
        private IGameSettingsManager _gameSettingsManager;

        private IGameSettings _gameSettings;

        public GameUI(IGameService gameService, IMusicPlayer musicPlayer, IGameSettingsManager gameSettingsManager)
        {
            _gameService = gameService;
            _musicPlayer = musicPlayer;
            _gameSettingsManager = gameSettingsManager;
            _gameSettings = _gameSettingsManager.LoadSettings<GameSettings>();
        }

        private void ShowMainMenu()
        {
            MenuItem[] menuItems = new MenuItem[]
            {
                new MenuItem(
                    "Старт",
                    onEnter: StartGame
                ),
                new MenuItem(
                    "Настройки",
                    onEnter: ShowSettingsMenu
                ),
                new MenuItem(
                    "Выход",
                    onEnter: Exit
                )
            };

            if (_gameSettings.MusicMute == false)
                _ = _musicPlayer.Play(MusicTrack.MusicGameMenu);

            ShowMenu("Главное меню", menuItems);
        }

        private void ShowSettingsMenu()
        {
            #region Громкость
            MenuItem volumeItem = new MenuItem(
                name: "Громкость",
                value: 50
            );

            volumeItem.OnLeft = () =>
            {
                if (volumeItem.Value is int)
                {
                    int value = Math.Max(0, (int)volumeItem.Value - 5);
                    volumeItem.Value = value;
                    _musicPlayer.SetVolume(value);
                    _gameSettings.MusicVolume = value;
                }
                else
                {
                    // Обработка исключения
                }
            };
            volumeItem.OnRight = () =>
            {
               if (volumeItem.Value is int)
                {
                    int value = Math.Min(100, (int)volumeItem.Value + 5);
                    volumeItem.Value = value;
                    _musicPlayer.SetVolume(value);
                    _gameSettings.MusicVolume = value;
                }
                else
                {
                    // Обработка исключения
                }
            };
            #endregion

            #region Музыка
            MenuItem musicItem = new MenuItem(
                name: "Музыка: ",
                value: _gameSettings.MusicMute ? "Выключена" : "Включена"
            );

            musicItem.OnLeft = MusicStateChange;
            musicItem.OnRight = MusicStateChange;

            void MusicStateChange()
            {
                if (musicItem.Value is string)
                {
                    if (!_gameSettings.MusicMute)
                    {
                        _musicPlayer.StopMusic();
                        musicItem.Value = "Выключена";
                        _gameSettings.MusicMute = true;
                    }
                    else
                    {
                        _musicPlayer.Play(MusicTrack.MusicGameMenu);
                        musicItem.Value = "Включена";
                        _gameSettings.MusicMute = false;
                    }
                } else
                {
                    //Обработка исключения
                }
            }
            #endregion

            var settingsMenuItems = new MenuItem[]
            {
                volumeItem,
                musicItem,
                new MenuItem (
                    name: "Правила игры",
                    onEnter: ShowGameRulesMenu
                ),
                new MenuItem (
                    name: "Назад",
                    onEnter: () => SetSettings()
                )
            };

            ShowMenu("Настройки", settingsMenuItems);

        }

        private void ShowGameRulesMenu()
        {
            #region Количество попыток
            MenuItem attemptsItem = new MenuItem(
                name: "Количество попыток",
                value: _gameSettings.Attempts
            );

            attemptsItem.OnLeft = () =>
            {
                if (attemptsItem.Value is int)
                {
                    int attempts = Math.Max(1, (int)attemptsItem.Value - 1);
                    attemptsItem.Value = attempts;
                    _gameSettings.Attempts = attempts;
                }
                else
                {
                    //Обработка исключения
                }
            };

            attemptsItem.OnRight = () =>
            {
                if (attemptsItem.Value is int)
                {
                    int attempts = Math.Min(20, (int)attemptsItem.Value + 1);
                    attemptsItem.Value = attempts;
                    _gameSettings.Attempts = attempts;
                }
                else
                {
                    //Обработка исключения
                }
            };
            #endregion

            #region Диапазон чисел
            MenuItem minRangeItem = new MenuItem(
                name: "Mин. диапазон чисел",
                value: _gameSettings.Range.MinValue
            );

            MenuItem maxRangeItem = new MenuItem(
                name: "Mакс. диапазон чисел",
                value: _gameSettings.Range.MaxValue
            );

            minRangeItem.OnLeft = () =>
            {
                int minValue = Math.Max(0, _gameSettings.Range.MinValue - 1);

                SetRangeValue(minValue, _gameSettings.Range.MaxValue);
            };

            minRangeItem.OnRight = () =>
            {
                int minValue = Math.Min(_gameSettings.Range.MaxValue - 10, _gameSettings.Range.MinValue + 1);

                SetRangeValue(minValue, _gameSettings.Range.MaxValue);
            };

            maxRangeItem.OnLeft = () =>
            {
                int maxValue = Math.Max(_gameSettings.Range.MinValue, _gameSettings.Range.MaxValue - 1);

                SetRangeValue(_gameSettings.Range.MinValue, maxValue);
            };

            maxRangeItem.OnRight = () =>
            {
                int maxValue = Math.Min(100, _gameSettings.Range.MaxValue + 1);

                SetRangeValue(_gameSettings.Range.MinValue, maxValue);
            };
            #endregion

            void SetRangeValue(int minValue, int maxValue)
            {
                minRangeItem.Value = minValue;
                maxRangeItem.Value = maxValue;
                _gameSettings.Range.MinValue = minValue;
                _gameSettings.Range.MaxValue = maxValue;
            }

            var rulesMenuItems = new MenuItem[]
            {
                attemptsItem,
                minRangeItem,
                maxRangeItem,
                new MenuItem (
                    name: "Назад",
                    onEnter: SetSettings
                )
            };

            ShowMenu("Правила игры", rulesMenuItems);
        }

        private void ShowGameEndMenu(string title)
        {
            var gameEndItems = new MenuItem[]
            {
                new MenuItem (
                    name: "Повторить",
                    onEnter: StartGame
                ),
                new MenuItem (
                    name: "Главное меню",
                    onEnter: ShowMainMenu
                ),
                new MenuItem (
                    name: "Выход",
                    onEnter: Exit
                )
            };

            ShowMenu(title, gameEndItems);
        }

        private void ShowMenu(string menuTitle, MenuItem[] menuItems)
        {
            string[] Maintitle =
{
            "██    ██  ███████    █████      █████     ██████  ██    ████",
            "██    ██  ██       ██   ██     ██  ██    ██   ██  ██   ██ ██",
            "████████  ██      ██    ██    ██   ██   ██    ██  ██  ██  ██",
            "      ██  ██      ████████  ██████████  ████████  ██ ██   ██",
            "████████  ██      ██    ██  ██      ██  ██    ██  ████    ██"
            };


            int index = 0;

            while (true)
            {
                Console.Clear();

                int screenWidth = Console.WindowWidth;
                int startY = Console.WindowHeight / 4;

                for (int i = 0; i < Maintitle.Length; i++)
                {
                    Console.SetCursorPosition((screenWidth - Maintitle[i].Length) / 2, startY + i);
                    Console.WriteLine(Maintitle[i]);
                }

                int screenHeight = Console.WindowHeight;
                screenWidth = Console.WindowWidth;

                int verticalOffset = screenHeight / 2 - menuItems.Length / 2;
                int horizontalOffset = (screenWidth / 2) - 10;

                Console.SetCursorPosition(horizontalOffset, verticalOffset);
                Console.WriteLine(menuTitle);
                verticalOffset += 2;

                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.SetCursorPosition(horizontalOffset, verticalOffset + i);

                    if (i == index)
                    {
                        string prefix = (i == index) ? "> " : "  ";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {menuItems[i].Name}{(menuItems[i].Value != null ? $": {menuItems[i].Value}" : "")}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuItems[i].Name}{(menuItems[i].Value != null ? $": {menuItems[i].Value}" : "")}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + menuItems.Length) % menuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % menuItems.Length;
                        break;
                    case ConsoleKey.LeftArrow:
                        menuItems[index].OnLeft?.Invoke();
                        break;
                    case ConsoleKey.RightArrow:
                        menuItems[index].OnRight?.Invoke();
                        break;
                    case ConsoleKey.Enter:
                        menuItems[index].OnEnter?.Invoke();
                        if (menuItems[index].Name == "Назад") return;
                        break;
                }
            }
        }

        public void SetSettings()
        {
            _gameSettingsManager.SaveSettings<IGameSettings>(_gameSettings);
            _gameService.SetSettings(_gameSettings);
        }

        public void Start()
        {
            ShowMainMenu();
        }

        public void StartGame()
        {
            _ = _musicPlayer.Play(MusicTrack.MusicGamePlay);
            _gameService.Start();
            Console.WriteLine("Игра началась!");
            EnterNumber();
        }

        public void GuessNumber(int number)
        {
            int? result = _gameService.GuessNumber(number);
            
            switch(result)
            {
                case -1:
                    Console.WriteLine($"Ваше число меньше. Осталось попыток: {_gameService.GetAttemptCount()}");
                    break;
                case 1:
                    Console.WriteLine($"Ваше число больше. Осталось попыток: {_gameService.GetAttemptCount()}");
                    break;
                case 0:
                    _ = _musicPlayer.Play(MusicTrack.MusicGameWin);
                    ShowGameEndMenu("Вы выйграли!");
                    break;
                case null:
                    _ = _musicPlayer.Play(MusicTrack.MusicGameOver);
                    ShowGameEndMenu($"Вы проиграли. Я загадал число: {_gameService.GetAnswer()}");
                    break;
            }

            if (result != 0) EnterNumber();
        }

        public void EnterNumber()
        {
            Console.Write("Введите число:");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                GuessNumber(number);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
