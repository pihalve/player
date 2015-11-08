using System.IO;
using Pihalve.Player.Tagging.Model;

namespace Pihalve.Player.Tagging
{
    public interface ITagReader
    {
        Id3Tag Read(FileInfo file);
    }
}
