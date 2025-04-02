using SOLID.Infrastructure.MusicPlayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Infrastructure.MusicPlayer.Interfaces
{
    interface IMusicPlayer
    {
        public Task Play(MusicTrack musicName);

        public void StopMusic();

        public void SetVolume(int volume);
    }
}
