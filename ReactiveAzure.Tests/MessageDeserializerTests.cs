using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //This is a kind of funny circular dependency - need a TypedQueueMessage to read from, and
            // a MessageDeserializer needs to take a QueueMessage for deserializing.
            var typedMessage = new TypedQueueMessage<TestMessage>(deserializer, messageContents);
            
            var result = typedMessage.GetValue();

            Assert.AreEqual("TestMessage", result.StringProperty);
            Assert.AreEqual(123, result.IntegerProperty);
        }
    }
}
