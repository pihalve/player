using System;

namespace Pihalve.Player.Library.Model
{
    public class Track
    {
        public Guid Id { get; set; }

        public int? Number { get; set; }
        public int? DiscNumber { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Genre { get; set; }
        public int? Year { get; set; }
        public string Comment { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTimeOffset Added { get; set; }
        public DateTimeOffset Modified { get; set; }
        public int PlayCount { get; set; }
        public DateTimeOffset LastPlayed { get; set; }
        public Uri Location { get; set; }
    }
}
