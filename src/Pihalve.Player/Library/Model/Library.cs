using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pihalve.Player.Library.Model
{
    public class Library
    {
        public ICollection<Track> Tracks { get; } = new HashSet<Track>();
        public ICollection<Playlist> Playlists { get; } = new SortedSet<Playlist>();
        public ICollection<SmartPlaylist> SmartPlaylists { get; } = new SortedSet<SmartPlaylist>();

        [IgnoreDataMember]
        public bool IsDirty { get; set; }
    }
}
