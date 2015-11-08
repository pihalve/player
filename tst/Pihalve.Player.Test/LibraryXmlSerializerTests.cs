using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Pihalve.Player.Library;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.Test
{
    [TestFixture]
    public class LibraryXmlSerializerTests
    {
        [Test]
        public void can_serialize_library()
        {
            //// Arrange
            const string libraryFilePath = @"library.xml";
            var track = new Track
            {
                Id = new Guid("{DF565C73-D3F0-4DC4-B9C4-5F9915A13921}"),
                Title = "Title",
                Added = new DateTimeOffset(2015, 11, 6, 13, 14, 15, new TimeSpan(1, 0, 0)),
                Duration = new TimeSpan(1042),
                Year = null,
                Location = @"C:\Some\Path"
            };
            var playlist = new Playlist
            {
                Id = new Guid("{B3D01D19-DC13-4F38-AE38-1176DBB2A094}"),
                Name = "Playlist",
                Items = { new PlaylistItem { SortNumber = 42, Track = track } }
            };
            var library = new Library.Model.Library
            {
                Tracks = { track },
                Playlists = { playlist }
            };
            var serializer = new LibraryXmlSerializer();

            //// Act
            serializer.Serialize(library, libraryFilePath);

            //// Assert
            var deserializedLibrary = serializer.Deserialize(libraryFilePath);
            deserializedLibrary.Tracks.First().Id.Should().Be(track.Id);
            deserializedLibrary.Tracks.First().Added.Should().Be(new DateTimeOffset(2015, 11, 6, 13, 14, 15, new TimeSpan(1, 0, 0)));
            deserializedLibrary.Tracks.First().Title.Should().Be(track.Title);
            deserializedLibrary.Tracks.First().Year.Should().NotHaveValue();
            deserializedLibrary.Tracks.First().Location.Should().Be(track.Location);

            deserializedLibrary.Playlists.First().Id.Should().Be(playlist.Id);
            deserializedLibrary.Playlists.First().Name.Should().Be(playlist.Name);
            deserializedLibrary.Playlists.First().Items.First().SortNumber.Should().Be(playlist.Items.First().SortNumber);
            deserializedLibrary.Playlists.First().Items.First().Track.Id.Should().Be(track.Id);
            deserializedLibrary.Playlists.First().Items.First().Track.Added.Should().Be(new DateTimeOffset(2015, 11, 6, 13, 14, 15, new TimeSpan(1, 0, 0)));
            deserializedLibrary.Playlists.First().Items.First().Track.Title.Should().Be(track.Title);
        }
    }
}
