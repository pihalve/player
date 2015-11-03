using System;
using System.Windows.Media;

namespace Pihalve.Player.UI
{
    public class WindowsMediaPlayer : IMediaPlayer
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        public void Open(Uri source)
        {
            _mediaPlayer.Open(source);
        }

        public void Play()
        {
            _mediaPlayer.Play();
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
        }

        public void Close()
        {
            _mediaPlayer.Close();
        }

        public Uri Source
        {
            get { return _mediaPlayer.Source; }
        }

        public double Volume
        {
            get { return _mediaPlayer.Volume; }
            set { _mediaPlayer.Volume = value; }
        }
    }
}
