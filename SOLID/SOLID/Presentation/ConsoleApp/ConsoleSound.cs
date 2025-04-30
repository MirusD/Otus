using SOLID.Domain.Enums;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Presentation.ConsoleApp.Enums;
using SOLID.Presentation.ConsoleApp.Interfaces;
using SOLID.Presentation.ConsoleApp.ViewModels;

namespace SOLID.Presentation.ConsoleApp
{
    class ConsoleSound
    {
        private readonly ISoundPlayer<SoundGameTrack> _soundPlayer;

        public ConsoleSound(ISoundPlayer<SoundGameTrack> soundPlayer, IGameController gameController)
        {
            gameController.Subscribe(async viewModel => await Task.Run(() => ConsoleSoundGamePlay(viewModel)));
            _soundPlayer = soundPlayer;

            var playList = new Dictionary<SoundGameTrack, string>
            {
                { SoundGameTrack.Menu, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Sounds", "dendy-laxity.mp3") },
                { SoundGameTrack.GamePlay, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Sounds", "dendy-chocolate.mp3") },
                { SoundGameTrack.GameOver, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Sounds", "dendy-game_over.mp3") },
                { SoundGameTrack.GameWin, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Sounds", "dendy-win.mp3") },
            };

            _soundPlayer.SetPlayList(playList);
        }

        public void Start()
        {
            _soundPlayer.Play(SoundGameTrack.Menu);
        }

        public void ConsoleSoundGamePlay(GamePlayViewModel gamePlayViewModel)
        {
            if (gamePlayViewModel.GameStatus == GuessResult.Correct)
            {
                _soundPlayer.Play(SoundGameTrack.GameWin);
                return;
            }

            if (gamePlayViewModel.GameStatus == GuessResult.GameOver)
            {
                _soundPlayer.Play(SoundGameTrack.GameOver);
                return;
            }
        }
    }
}
