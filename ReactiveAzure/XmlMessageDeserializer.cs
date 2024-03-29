using System.IO;
using System.Xml.Serialization;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    /// <summary>
    /// Uses an <see cref="XmlSerializer"/> internally to deserialize the given message.
    /// </summary>
    public class XmlMessageDeserializer<T> : IMessageDeserializer<T>
    {
        public T DeserializeMessage(CloudQueueMessage message)
        {
            return DeserializeFromString(message.AsString);
        }

        private static T DeserializeFromString(string message)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new StringReader(message))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}