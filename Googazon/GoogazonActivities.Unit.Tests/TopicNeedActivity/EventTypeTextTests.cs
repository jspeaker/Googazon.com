using FluentAssertions;
using GoogazonActivities.Models;
using GoogazonActivities.TopicNeedActivity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonActivities.Unit.Tests.TopicNeedActivity
{
    [TestClass]
    public class EventTypeTextTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenRecognizedEventType_WhenAskingForTextAsEnum_ThenItShouldReturnCorrectValue()
        {
            // arrange
            EventTypeText eventTypeText = new EventTypeText("CustomerService");

            // act
            EventType actual = eventTypeText.AsEnum();

            // assert
            actual.Should().Be(EventType.CustomerService);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenIncorrectlyCasedRecognizedEventType_WhenAskingForTextAsEnum_ThenItShouldReturnCorrectValue()
        {
            // arrange
            EventTypeText eventTypeText = new EventTypeText("cusTomErSerVice");

            // act
            EventType actual = eventTypeText.AsEnum();

            // assert
            actual.Should().Be(EventType.CustomerService);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenUnrecognizedEventType_WhenAskingForTextAsEnum_ThenItShouldReturnCorrectValue()
        {
            // arrange
            EventTypeText eventTypeText = new EventTypeText("DammitBobby!");

            // act
            EventType actual = eventTypeText.AsEnum();

            // assert
            actual.Should().Be(EventType.None);
        }
    }
}