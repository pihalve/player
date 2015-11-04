﻿using System;
using System.Collections.Generic;

namespace Pihalve.Player.Library.Model
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlaylistItem> Items { get; } = new SortedSet<PlaylistItem>();
    }
}
