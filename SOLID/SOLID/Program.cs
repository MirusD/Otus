using SOLID.BLL.Domain.Settings;
using SOLID.BLL.Services.Implementations;
using SOLID.Infrastructure;
using SOLID.Infrastructure.MusicPlayer;
using SOLID.Infrastructure.MusicPlayer.Enums;
using SOLID.Infrastructure.Settings;

namespace SOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var playList = new Dictionary<MusicTrack, string>
            {
                { MusicTrack.MusicGameMenu, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/sound", "dendy-laxity.mp3") },
                { MusicTrack.MusicGamePlay, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/sound", "dendy-chocolate.mp3") },
                { MusicTrack.MusicGameOver, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/sound", "dendy-game_over.mp3") },
                { MusicTrack.MusicGameWin, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/sound", "dendy-win.mp3") },
            };
            GameUI gameUI = new GameUI(new GameService(), new MusicPlayer(playList), new GameSettingsManager());
            gameUI.Start();
        }
    }
}

/*
 * ============================== Отчёт ==================================
 *  
 *  - Single Responsibility Principle
 *  
 *  1. В Domain храниться сущности игры.
 *  2. В Services храниться код для логики игры. GameService инкапсулирует в себе методы для управления игрой.
 *  3. Взаимодействие с пользователем производиться через GameUI.
 *  4. Воспроизведение музыки производиться в MusicPlayer.
 *  
 *  - Open/Close Principle
 *  
 *  В GameUI можно передать разные реализации IGameService и IMusicService, GameSettingsManager без изменения самого класса GameUI.
 *  
 *  - Liskov Substitution Principle
 *  
 *  В принцип LSP гласит что мы можем вместо родительских классов или интерфейсов подставлять наследников и все должно
 *  работать исправно, по сути классы GameService, MusicService и GameSettingsManager наследуються от сових интерфейсов 
 *  и мы подставляем инстансы классов вместо интерфейсов. А также мы може отнаследоваться от GameSetting и подставить 
 *  подкласс в GameSettingsManager и все должно работать.
 *  
 *  - Interface Segregation Principle
 *  
 *  Есть отдельный интерфейс IGameService, IGameSettings, IMusicPlayer, IGameSettingsManager
 *  они разбиты по назначению и не содержат в себе методов которые не используються.
 *  
 *  - Dependency Inversion Principle
 *  
 *  GameUI зависит от IGameService, то есть сущности верхнего уровня не зависят от 
 *  сущностей верхнего уровня, детали зависят от абстракции.
 *  
 */
