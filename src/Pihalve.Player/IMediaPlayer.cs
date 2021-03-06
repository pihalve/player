﻿using System;

namespace Pihalve.Player
{
    public interface IMediaPlayer
    {
        void Open(Uri source);
        void Play();
        void Pause();
        void Stop();
        void Close();
        Uri Source { get; }
        double Volume { get; set; }
    }
}
