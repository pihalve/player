using System.IO;
using Pihalve.Player.Tagging.Model;

namespace Pihalve.Player.Tagging
{
    public class TagLibSharpId3TagReader : ITagReader
    {
        public Id3Tag Read(FileInfo file)
        {
            using (var tagFile = TagLib.File.Create(file.FullName))
            {
                if (!tagFile.Tag.IsEmpty)
                {
                    return CreateId3Tag(tagFile.Tag);
                }
            }

            return null;
        }

        private static Id3Tag CreateId3Tag(TagLib.Tag tag)
        {
            var id3Tag = new Id3Tag
            {
                Number = tag.Track == 0 ? null : (int?)tag.Track,
                DiscNumber = tag.Disc == 0 ? null : (int?)tag.Disc,
                Title = tag.Title,
                Album = tag.Album,
                Year = tag.Year == 0 ? null : (int?)tag.Year,
                Comment = tag.Comment
            };

            id3Tag.Artists.AddRange(tag.Performers);
            id3Tag.AlbumArtists.AddRange(tag.AlbumArtists);
            id3Tag.Genres.AddRange(tag.Genres);

            return id3Tag;
        }
    }
}
