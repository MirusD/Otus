using SOLID.Infrastructure.Settings.Interfaces;
using SOLID.Presentation.ConsoleApp.Menu.Items;
using SOLID.Presentation.ConsoleApp.Commands;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;

namespace SOLID.Presentation.ConsoleApp.Menu
{
    class MenuBuilder
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ISettingsProvider _settingsProvider;

        public MenuBuilder(ICommandFactory commandFactory, ISettingsProvider settingsProvider)
        {
            _commandFactory = commandFactory;
            _settingsProvider = settingsProvider;
        }

        public Menu BuildMainMenu()
        {
            return new Menu("Главное меню", new MenuItem[]
            {
                new ActionMenuItem
                (
                    text : "Старт",
                    onSelect: _commandFactory.Create(CommandType.StartGame)
                ),

                new SubMenuItem
                (
                    text: "Настройки",
                    buildsubMenu: BuildSettingsMenu
                ),

                new ActionMenuItem
                (
                    text: "Выход",
                    onSelect: _commandFactory.Create(CommandType.ExitGame)
                )
            });
        }

        public Menu BuildSettingsMenu()
        {
            return new Menu("Настройки", new MenuItem[]
            {
                new SettingMenuItem
                (
                    text: "Громкость",
                    getValue: () => _settingsProvider.SoundSettings.SoundVolume.ToString(),
                    onLeft : _commandFactory.Create(CommandType.VolumeDown),
                    onRight : _commandFactory.Create(CommandType.VolumeUp)
                ),

                new SettingMenuItem
                (
                    text :"Музыка",
                    getValue: () => _settingsProvider.SoundSettings.SoundMute ? "Выкл" : "Вкл",
                    onLeft: _commandFactory.Create(CommandType.ToggleSoundMute),
                    onRight: _commandFactory.Create(CommandType.ToggleSoundMute)
                ),

                new SettingMenuItem
                (
                    text:"Попытки",
                    getValue :() => _settingsProvider.GameSettings.MaxAttemps.ToString(),
                    onLeft: _commandFactory.Create(CommandType.AttemptsDown),
                    onRight: _commandFactory.Create(CommandType.AttemptsUp)
                ),

                new SettingMenuItem
                (
                    text:"Минимум",
                    getValue :() => _settingsProvider.GameSettings.MinValue.ToString(),
                    onLeft: _commandFactory.Create(CommandType.MinValueDown),
                    onRight: _commandFactory.Create(CommandType.MinValueUp)
                ),

                new SettingMenuItem
                (
                    text: "Максимум",
                    getValue :() => _settingsProvider.GameSettings.MaxValue.ToString(),
                    onLeft: _commandFactory.Create(CommandType.MaxValueDown),
                    onRight: _commandFactory.Create(CommandType.MaxValueUp)
                ),

                new SubMenuItem("Назад", BuildMainMenu)
            });
        }

        public Menu BuildGameEndMenu(string title)
        {
            return new Menu(title, new MenuItem[]
            {
                new ActionMenuItem
                (
                    text: "Повторить", 
                    onSelect: _commandFactory.Create(CommandType.StartGame)
                ),

                new SubMenuItem
                (
                    text: "Главное меню", 
                    buildsubMenu: BuildMainMenu
                ),

                new ActionMenuItem
                (
                    text : "Выход", 
                    onSelect: _commandFactory.Create(CommandType.ExitGame)
                )
            });
        }
    }
}
