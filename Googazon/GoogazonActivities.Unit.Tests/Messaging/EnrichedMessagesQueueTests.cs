using FluentAssertions;
using GoogazonActivities.Messaging;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoogazonActivities.Unit.Tests.Messaging
{
    [TestClass]
    public class EnrichedMessagesQueueTests
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://fake-googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=asdfasdf12341234asdfasdf12341234=");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessagesQueue_WhenAskingForClient_ThenItShouldReturnQueueClientInstance()
        {
            // arrange // act
            IQueueClient queueClient = EnrichedMessagesQueue.Client;

            // assert
            queueClient.Should().BeOfType<QueueClient>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessagesQueue_WhenAskingForClientTwice_ThenItShouldReturnSameQueueClientInstance()
        {
            // arrange // act
            IQueueClient queueClient1 = EnrichedMessagesQueue.Client;
            IQueueClient queueClient2 = EnrichedMessagesQueue.Client;

            // assert
            queueClient1.Should().Be(queueClient2);
        }
    }
}