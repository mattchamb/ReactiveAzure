using System.IO;
using System.Xml.Serialization;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public class XmlMessageSerializer<T> : IMessageSerializer<T>
    {
        public CloudQueueMessage SerializeMessage(T value)
        {
            return new CloudQueueMessage(SerializeToString(value));
        }

        private static string SerializeToString(T value)
        {
            var serializer = new XmlSerializer(typeof (T));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, value);
                return stream.ToString();
            }
        }
    }
}