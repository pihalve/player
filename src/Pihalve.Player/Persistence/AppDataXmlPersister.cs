using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Pihalve.Player.Persistence
{
    public class AppDataXmlPersister<T> : IAppDataPersister<T>
    {
        private readonly string _productAppDataFolderPath;

        public AppDataXmlPersister(string productAppDataFolderPath)
        {
            _productAppDataFolderPath = productAppDataFolderPath;
        }

        public void Save(T applicationData, string outputFileName)
        {
            var outputFilePath = Path.Combine(_productAppDataFolderPath, outputFileName);

            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = XmlWriter.Create(outputFilePath, xmlWriterSettings))
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(writer, applicationData);
            }
        }

        public T Load(string inputFileName)
        {
            var inputFilePath = Path.Combine(_productAppDataFolderPath, inputFileName);

            using (var reader = XmlReader.Create(inputFilePath))
            {
                var serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(reader);
            }
        }
    }
}
