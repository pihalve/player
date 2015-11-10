using System.IO;
using Pihalve.Player.Tagging.Model;

namespace Pihalve.Player.Tagging
{
    public interface ITagReader
    {
        Tag Read(FileInfo file);
    }
}
