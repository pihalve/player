using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.ViewModels
{
    public class FieldViewModel
    {
        public static readonly Dictionary<Expression<Func<Track, object>>, string> Translations = new Dictionary<Expression<Func<Track, object>>, string>
        {
            { x => x.DiscNumber, "Disc" },
            { x => x.Number, "Track" },
            { x => x.Title , "Title" },
            { x => x.Artists, "Artist" },
            { x => x.Album, "Album" },
            { x => x.AlbumArtists, "Album artist" },
            { x => x.Genres, "Genre" },
            { x => x.Year, "Year" },
            { x => x.Comment, "Comment" },
            { x => x.Duration, "Duration" },
            { x => x.Added, "Added" },
            { x => x.Modified, "Modified" },
            { x => x.PlayCount, "Plays" },
            { x => x.LastPlayed, "Last played" },
            { x => x.Rating, "Rating" }
        };

        public FieldViewModel(Expression<Func<Track, object>> value)
        {
            Value = value;
        }

        public Expression<Func<Track, object>> Value { get; }

        public string DisplayName => Translations[Value];
    }
}
