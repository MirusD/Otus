using Microsoft.Extensions.DependencyInjection;
using SOLID.Application.Interfaces;
using SOLID.Application.Services;
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Factories;
using SOLID.Infrastructure.MusicPlayer;
using SOLID.Infrastructure.Services;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;
using SOLID.Infrastructure.Settings;
using SOLID.Infrastructure.Settings.Interfaces;
using SOLID.Presentation;
using SOLID.Presentation.ConsoleApp;
using SOLID.Presentation.ConsoleApp.Commands.Factories;
using SOLID.Presentation.ConsoleApp.Commands.Interfaces;
using SOLID.Presentation.ConsoleApp.Controller;
using SOLID.Presentation.ConsoleApp.Enums;
using SOLID.Presentation.ConsoleApp.Menu;
using SOLID.Presentation.ConsoleApp.Sound;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Infrastructure.Configuration
{
    class DependencyInjection
    {
        public static void ConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton<IGameSettings, GameSettings>();

            services.AddSingleton<ISoundSettings, SoundSettings>();

            services.AddSingleton<ISettingsProvider, SettingsProvider>();

            services.AddSingleton<ICommandFactory, CommandFactory>();

            services.AddSingleton<IGameFactory, GameFactory>();

            services.AddSingleton<GameController, GameController>();

            services.AddSingleton<IGameService, GuessNumberGameService>();

            services.AddSingleton<INumberGenerator, RandomNumberGenerator>();

            services.AddSingleton<MenuBuilder, MenuBuilder>();

            services.AddSingleton<ISoundPlayer<SoundGameTrack>, SoundPlayer<SoundGameTrack>>();

            services.AddSingleton<AppConsole, AppConsole>();

            services.AddSingleton<PresentationRoot, PresentationRoot>();

            services.AddSingleton<ConsoleSound, ConsoleSound>();

            services.AddSingleton<GamePlayViewModel, GamePlayViewModel>();

        }
    }
}
