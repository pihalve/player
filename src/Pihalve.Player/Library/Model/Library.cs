using System.Collections.Generic;

namespace Pihalve.Player.Library.Model
{
    public class Library
    {
        public ICollection<Track> Tracks { get; } = new HashSet<Track>();
        public ICollection<Playlist> Playlists { get; } = new SortedSet<Playlist>();
    }
}
