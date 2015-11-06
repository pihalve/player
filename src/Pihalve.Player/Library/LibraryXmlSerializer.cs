using System.Runtime.Serialization;
using System.Xml;

namespace Pihalve.Player.Library
{
    public class LibraryXmlSerializer : ILibrarySerializer
    {
        public void Serialize(Model.Library library, string outputFilePath)
        {
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = XmlWriter.Create(outputFilePath, xmlWriterSettings))
            {
                var serializer = new DataContractSerializer(typeof(Model.Library));
                serializer.WriteObject(writer, library);
            }
        }

        public Model.Library Deserialize(string inputFilePath)
        {
            using (var reader = XmlReader.Create(inputFilePath))
            {
                var serializer = new DataContractSerializer(typeof (Model.Library));
                return (Model.Library)serializer.ReadObject(reader);
            }
        }
    }
}
