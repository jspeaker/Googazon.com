using FluentAssertions;
using GoogazonActivities.Messaging;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace GoogazonActivities.Unit.Tests.Messaging
{
    [TestClass]
    public class EventMessageBodyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenValidBodyWithCsTopic_WhenUsingAskingIsCsEventType_ThenItShouldReturnTrue()
        {
            // arrange
            const string expected = "{'eventType':'customerservice','need':'accessory'}";
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dammitbobby");
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            bool actual = eventMessageBody.IsEventType(EventType.CustomerService);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenValidBodyWithUnknownTopic_WhenUsingAskingIsCsEventType_ThenItShouldReturnTrue()
        {
            // arrange
            const string expected = "{'eventType':'propane','need':'accessory'}";
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dammitbobby");
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            bool actual = eventMessageBody.IsEventType(EventType.CustomerService);

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenValidBodyWithUnknownTopic_WhenUsingAskingIsNoneEventType_ThenItShouldReturnTrue()
        {
            // arrange
            const string expected = "{'eventType':'propane','need':'accessory'}";
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dammitbobby");
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            bool actual = eventMessageBody.IsEventType(EventType.None);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenValidBody_WhenUsingAskingForServiceBusMessage_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "{'eventType':'propane','need':'accessory'}";
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dammitbobby");
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            ServiceBusMessage actual = eventMessageBody.AsServiceBusMessage();

            // assert
            actual.Should().BeOfType<ServiceBusMessage>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenInvalidBody_WhenUsingAskingForServiceBusMessage_ThenItShouldShouldReturnNullServiceBusMessage()
        {
            // arrange
            const string expected = "{'businessName':'Strickland Propane, LLC'}";
            Environment.SetEnvironmentVariable("ServiceBusConnectionString", "Endpoint=sb://googazon-rivers.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dammitbobby");
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            ServiceBusMessage actual = eventMessageBody.AsServiceBusMessage();

            // assert
            actual.Should().BeOfType<NullServiceBusMessage>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWellFormedBody_WhenUsingImplicitOperator_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "{'Dammit':'Bobby!'}";
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            string actual = eventMessageBody;

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWellFormedBody_WhenUsingImplicitOperator_ThenItShouldNotThrow()
        {
            // arrange
            string expected = string.Empty;
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(ArraySegment<byte>.Empty));

            // act
            string actual = eventMessageBody;

            // assert
            actual.Should().Be(expected);
        }
    }
}