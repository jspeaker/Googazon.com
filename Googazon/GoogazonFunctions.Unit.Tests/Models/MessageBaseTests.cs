using FluentAssertions;
using GoogazonFunctions.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Text;

namespace GoogazonFunctions.Unit.Tests.Models
{
    [TestClass]
    public class MessageBaseTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenHydratedObject_WhenAskingForEventData_TheItShouldReturnCorrectResult()
        {
            // arrange
            const string need = "offer";
            const EventType eventType = EventType.Marketing;
            MessageBaseImplementation message = new MessageBaseImplementation(eventType, need);

            // act
            EventData actual = message.EventData();

            // assert
            dynamic dynamicActual = JsonConvert.DeserializeObject<ExpandoObject>(Encoding.UTF8.GetString(actual.Body.Array));
            ((string) dynamicActual.eventType).Should().Be(eventType.ToString());
            ((string) dynamicActual.need).Should().Be(need);
            ((DateTime) dynamicActual.createdDateTime).Should().BeAfter(DateTime.UtcNow.AddMilliseconds(-500));
            ((string) dynamicActual.id).Should().HaveLength(36);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenHydratedObject_WhenAskingForUniqueIdentifier_TheItShouldReturnGuid()
        {
            // arrange
            MessageBaseImplementation message = new MessageBaseImplementation(EventType.Marketing, "offer");

            // act
            string actual = message.UniqueIdentifier();

            // assert
            Guid guid = Guid.Parse(actual);
            guid.Should().NotBeEmpty();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenHydratedObject_WhenAskingForTopic_TheItShouldReturnNeed()
        {
            // arrange
            const string expected = "offer";
            MessageBaseImplementation message = new MessageBaseImplementation(EventType.Marketing, expected);

            // act
            string actual = message.Topic();

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenMarketingEventType_WhenAskingIsOtherEventType_TheItShouldReturnCorrectResult()
        {
            // arrange
            MessageBaseImplementation message = new MessageBaseImplementation(EventType.Marketing, "offer");

            // act
            bool actual = message.IsEventType(EventType.CustomerService);

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenMarketingEventType_WhenAskingIsSameEventType_TheItShouldReturnCorrectResult()
        {
            // arrange
            MessageBaseImplementation message = new MessageBaseImplementation(EventType.Marketing, "offer");

            // act
            bool actual = message.IsEventType(EventType.Marketing);

            // assert
            actual.Should().BeTrue();
        }
    }
}