using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Pihalve.Player.Library
{
    public class NewLibraryBuilder : ILibraryBuilder
    {
        private readonly Uri _libraryPath;
        private readonly ITrackFactory _trackFactory;
        private readonly BackgroundWorker _backgroundWorker;

        public Model.Library Library { get; }

        public NewLibraryBuilder(Uri libraryPath, ITrackFactory trackFactory, BackgroundWorker backgroundWorker)
        {
            _libraryPath = libraryPath;
            _trackFactory = trackFactory;
            _backgroundWorker = backgroundWorker;
            Library = new Model.Library
            {
                IsDirty = true
            };
        }

        public void BuildTrackList()
        {
            var files = new DirectoryInfo(_libraryPath.AbsolutePath).EnumerateFiles("*.mp3", SearchOption.AllDirectories).ToList();
            double totalCount = files.Count;
            double currentCount = 0;
            foreach (var file in files)
            {
                Library.Tracks.Add(_trackFactory.Create(file));

                currentCount++;
                double percentage = currentCount / totalCount * 100;
                _backgroundWorker.ReportProgress((int)percentage);
            }
        }

        public void BuildPlaylists()
        {
        }
    }
}
