using System;

namespace Pihalve.Player.Library.Model
{
    public class PlaylistItem : IComparable<PlaylistItem>
    {
        public int SortNumber { get; set; }
        public Track Track { get; set; }

        public int CompareTo(PlaylistItem other)
        {
            return SortNumber.CompareTo(other);
        }
    }
}
