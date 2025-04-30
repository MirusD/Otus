using NAudio.Wave;
using SOLID.Infrastructure.Services.SoundPlayer.Interfaces;
using SOLID.Infrastructure.Services.SoundPlayer.Settings;

namespace SOLID.Infrastructure.MusicPlayer
{
    public class SoundPlayer<T> : ISoundPlayer<T>
        where T : Enum
    {
        private WaveOutEvent? _outputDevice;
        private MediaFoundationReader? _audioFile;
        private WaveChannel32? _volumeControl;
        private Dictionary<T, string> _playList = new();
        private ISoundSettings _soundSettings;

        public int Volume
        {
            get => _soundSettings.SoundVolume;
            set => _soundSettings.SoundVolume = Math.Clamp(value, 0, 100);
        }

        public bool Mute
        {
            get => _soundSettings.SoundMute;
            set => _soundSettings.SoundMute = value;
        }

        public SoundPlayer(ISoundSettings soundSettings)
        {
            _soundSettings = soundSettings;
        }

        public void SetPlayList(Dictionary<T, string> playList)
        {
            _playList = playList;
        }

        public async Task Play(T musicName)
        {
            StopMusic();

            if (!_playList.TryGetValue(musicName, out var filePath))
                throw new ArgumentException($"Файл для {musicName} не найден в плейлисте.");

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
                    if (_volumeControl != null)
                    {
                        _volumeControl.Volume = Volume / 100f;
                    }
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

            _outputDevice = null;
            _audioFile = null;
            _volumeControl = null;
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
