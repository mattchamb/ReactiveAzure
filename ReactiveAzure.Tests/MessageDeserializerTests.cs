using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;

namespace ReactiveAzure.Tests
{
    [TestFixture]
    public class MessageDeserializerTests
    {
        [Test]
        public void DeserializeMessage_WithValidXml_ReturnsCorrectObjectWithProperties()
        {
            const string messageContents = "<TestMessage>" +
                                           "<IntegerProperty>123</IntegerProperty>" +
                                           "<StringProperty>TestMessage</StringProperty>" +
                                           "</TestMessage>";
            var deserializer = new XmlMessageDeserializer<TestMessage>();

            var result = deserializer.DeserializeMessage(new CloudQueueMessage(messageContents));

            Assert.AreEqual("TestMessage", result.StringProperty);
            Assert.AreEqual(123, result.IntegerProperty);
        }
    }
}
