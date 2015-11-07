using System;
using System.IO;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.Library
{
    public class TrackFactory : ITrackFactory
    {
        //TODO: finish implementation

        public Track Create(FileInfo file)
        {
            var track = new Track
            {
                Id = new Guid(),
                Title = file.Name,
                Location = new Uri(file.FullName)
            };

            return track;
        }
    }
}
