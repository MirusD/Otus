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
using SOLID.Presentation.ConsoleApp.Enums;
using SOLID.Presentation.ConsoleApp.Interfaces;
using SOLID.Presentation.ConsoleApp.Menu;

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

            services.AddTransient<IGameFactory, GameFactory>();

            services.AddSingleton<IGameController, GameController>();

            services.AddSingleton<IGameService, GuessNumberGameService>();

            services.AddSingleton<INumberGenerator, RandomNumberGenerator>();

            services.AddSingleton<ConsoleUI, ConsoleUI>();

            services.AddSingleton<MenuBuilder, MenuBuilder>();

            services.AddSingleton<ISoundPlayer<SoundGameTrack>, SoundPlayer<SoundGameTrack>>();

            services.AddSingleton<IGameController, GameController>();

            services.AddSingleton<ConsoleUI, ConsoleUI>();

            services.AddSingleton<ConsoleMain, ConsoleMain>();

            services.AddSingleton<PresentationRoot, PresentationRoot>();

            services.AddSingleton<ConsoleSound, ConsoleSound>();

        }
    }
}
