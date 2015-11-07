using System.IO;
using Pihalve.Player.Library.Model;

namespace Pihalve.Player.Library
{
    public interface ITrackFactory
    {
        Track Create(FileInfo file);
    }
}
