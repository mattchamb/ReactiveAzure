using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Moq;
using NUnit.Framework;

namespace ReactiveAzure.Tests
{
    [TestFixture]
    public class ReactiveAzureTests
    {

        [Test]
        public void MessageReceived_OnNotificationWithNotificationsEnabled_ReadsValueFromQueue()
        {
            /*var expected = new TestMessage() {IntegerProperty = 123, StringProperty = "TEST"};

            var queueMessageMock = new Mock<ITypedQueueMessage<TestMessage>>();
            queueMessageMock.Setup(mock => mock.GetValue()).Returns(expected);

            var messageReaderMock = new Mock<IQueueMessageReader<TestMessage>>();
            messageReaderMock.Setup(mock => mock.GetMessage()).Returns(queueMessageMock.Object);

            var testNotifier = new TestNotifier();
            var ra = new ReactiveAzure<TestMessage>(testNotifier, messageReaderMock.Object);
            ra.BeginNotifications();

            TestMessage result = null;
            ra.MessageReceived += message => { result = message.GetValue(); };

            testNotifier.ForceElapse();

            Assert.AreSame(expected, result);*/
            Assert.Fail("Fix this test after refactoring.");
        }

        [Test]
        public void MessageReceived_OnNotificationWithNotificationsDisabled_DoesntReadValueFromQueue()
        {
            /*var expected = new TestMessage() { IntegerProperty = 123, StringProperty = "TEST" };

            var queueMessageMock = new Mock<ITypedQueueMessage<TestMessage>>();
            queueMessageMock.Setup(mock => mock.GetValue()).Returns(expected);

            var messageReaderMock = new Mock<IQueueMessageReader<TestMessage>>();
            messageReaderMock.Setup(mock => mock.GetMessage()).Returns(queueMessageMock.Object);

            var testNotifier = new TestNotifier();
            var ra = new ReactiveAzure<TestMessage>(testNotifier, messageReaderMock.Object);
            ra.EndNotifications();

            int callCount = 0;
            ra.MessageReceived += message => { callCount++; };

            testNotifier.ForceElapse();

            Assert.AreEqual(0, callCount);*/
            Assert.Fail("Fix this test after refactoring.");
        }
    }
}
