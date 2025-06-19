using SOLID.Application.Interfaces;
using SOLID.Domain.Enums;
using SOLID.Domain.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Presentation.ConsoleApp.Enums;

namespace SOLID.Presentation.ConsoleApp.Sound
{
    class ConsoleSound : IGameObserver
    {
        private readonly ISoundPlayer<SoundGameTrack> _soundPlayer;

        public ConsoleSound(ISoundPlayer<SoundGameTrack> soundPlayer)
        {
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

        public void PlayMainMenuSound()
        {
            _soundPlayer.Play(SoundGameTrack.Menu);
        }

        public void OnGameStateChanged(IGameState state)
        {
            if (state.GameStatus == GameStatus.Start && state.GuessResult == null)
            {
                _soundPlayer.Play(SoundGameTrack.GamePlay);
            }

            if (state.GuessResult == GuessResult.Correct)
            {
                _soundPlayer.Play(SoundGameTrack.GameWin);
            }
            else if (state.Attempts == 0)
            {
                _soundPlayer.Play(SoundGameTrack.GameOver);
            }
        }
    }
}
