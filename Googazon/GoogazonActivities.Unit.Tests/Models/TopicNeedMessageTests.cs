using FluentAssertions;
using GoogazonActivities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonActivities.Unit.Tests.Models
{
    [TestClass]
    public class TopicNeedMessageTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionIdInMessage_WhenAskingForClientIdentifier_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "connection-id";
            ITopicNeedMessage topicNeedMessage = new TopicNeedMessage(expected, "id", "topic", "need");

            // act
            string actual = topicNeedMessage.ClientIdentifier();

            // assert
            actual.Should().Be(expected);
        }

    }
}