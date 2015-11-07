using System;
using System.IO;

namespace Pihalve.Player.Library
{
    public class NewLibraryBuilder : ILibraryBuilder
    {
        private readonly Uri _libraryPath;
        private readonly ITrackFactory _trackFactory;

        public Model.Library Library { get; }

        public NewLibraryBuilder(Uri libraryPath, ITrackFactory trackFactory)
        {
            _libraryPath = libraryPath;
            _trackFactory = trackFactory;
            Library = new Model.Library();
        }

        public void BuildTrackList()
        {
            var files = new DirectoryInfo(_libraryPath.AbsolutePath).EnumerateFiles("*.mp3", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Library.Tracks.Add(_trackFactory.Create(file));
            }
        }

        public void BuildPlaylists()
        {
        }
    }
}
