using System.Xml.Serialization;

namespace ReactiveAzure.Tests
{
    [XmlRoot]
    public class TestMessage
    {
        [XmlElement]
        public string StringProperty { get; set; }
        [XmlElement]
        public int IntegerProperty { get; set; }
    }
}