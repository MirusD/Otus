using SOLID.Infrastructure.Settings.Interfaces;
using SOLID.Presentation.ConsoleApp.Commands.Factories;
using SOLID.Presentation.ConsoleApp.Menu.Items;

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
                    onSelect: _commandFactory.CreateStartGameCommand()
                ),

                new SubMenuItem
                (
                    text: "Настройки", 
                    buildsubMenu: BuildSettingsMenu
                ),

                new ActionMenuItem
                (
                    text: "Выход",
                    onSelect: _commandFactory.CreateExitGameCommand()
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
                    onLeft : _commandFactory.CreateChangeVolumeCommand(-1),
                    onRight : _commandFactory.CreateChangeVolumeCommand(1)
                ),

                new SettingMenuItem
                (
                    text :"Музыка",
                    getValue: () => _settingsProvider.SoundSettings.SoundMute ? "Выкл" : "Вкл",
                    onLeft: _commandFactory.CreateToggleSoundMuteCommand(),
                    onRight: _commandFactory.CreateToggleSoundMuteCommand()
                ),

                new SettingMenuItem
                (
                    text:"Попытки",
                    getValue :() => _settingsProvider.GameSettings.MaxAttemps.ToString(),
                    onLeft: _commandFactory.CreateChangeAttemptsCommand(-1),
                    onRight: _commandFactory.CreateChangeAttemptsCommand(1)
                ),

                new SettingMenuItem
                (
                    text:"Минимум",
                    getValue :() => _settingsProvider.GameSettings.MinValue.ToString(),
                    onLeft: _commandFactory.CreateChangeMinValueCommand(-1),
                    onRight: _commandFactory.CreateChangeMinValueCommand(1)
                ),

                new SettingMenuItem
                (
                    text: "Максимум",
                    getValue :() => _settingsProvider.GameSettings.MaxValue.ToString(),
                    onLeft: _commandFactory.CreateChangeMaxValueCommand(-1),
                    onRight: _commandFactory.CreateChangeMaxValueCommand(1)
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
                    onSelect: _commandFactory.CreateStartGameCommand()
                ),

                new SubMenuItem
                (
                    text: "Главное меню", 
                    buildsubMenu: BuildMainMenu
                ),

                new ActionMenuItem
                (
                    text : "Выход", 
                    onSelect: _commandFactory.CreateExitGameCommand()
                )
            });
        }
    }
}
