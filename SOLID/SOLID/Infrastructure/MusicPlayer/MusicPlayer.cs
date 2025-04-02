using NAudio.Wave;
using SOLID.Infrastructure.MusicPlayer.Enums;
using SOLID.Infrastructure.MusicPlayer.Interfaces;

namespace SOLID.Infrastructure.MusicPlayer
{
    class MusicPlayer : IMusicPlayer
    {
        private WaveOutEvent? _outputDevice;
        private MediaFoundationReader? _audioFile;
        private WaveChannel32? _volumeControl;
        private Dictionary<MusicTrack, string> _playList;

        public int Volume { get; set; } = 30;

        public MusicPlayer(Dictionary<MusicTrack, string> playList)
        {
            _playList = playList;
        }

        public async Task Play(MusicTrack musicName)
        {
            StopMusic();

            var filePath = _playList[musicName];
            _audioFile = new MediaFoundationReader(filePath);

            _volumeControl = new WaveChannel32(_audioFile)
            {
                Volume = Volume / 100f
            };

            _outputDevice = new WaveOutEvent();
            _outputDevice.Init(_volumeControl);
            _outputDevice.Play();

            await Task.Run(() =>
            {
                while (_outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    _volumeControl.Volume = Volume / 100f;
                    Task.Delay(500).Wait();
                }
            });
        }

        public void StopMusic()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
            _volumeControl?.Dispose();
        }

        public void SetVolume(int volume)
        {
            Volume = Math.Clamp(volume, 0, 100);
            if (_volumeControl != null)
            {
                _volumeControl.Volume = Volume / 100f;
            }
        }
    }
}
