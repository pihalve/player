namespace Pihalve.Player.Library
{
    public interface ILibrarySerializer
    {
        void Serialize(Model.Library library, string outputFilePath);
        Model.Library Deserialize(string inputFilePath);
    }
}
