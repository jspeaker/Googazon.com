using FluentAssertions;
using GoogazonActivities.Messaging;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace GoogazonActivities.Unit.Tests.Messaging
{
    [TestClass]
    public class TopicTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenTopicInMessageBody_WhenCreatingTopic_ThenItShouldHydrateIt()
        {
            // arrange
            const string expected = "Strickland Propane";
            EventData eventData = new EventData(Encoding.UTF8.GetBytes($"{{'EventType':'{expected}'}}"));
            EventMessageBody eventMessageBody = new EventMessageBody(eventData);

            // act
            Topic topic = new Topic(eventMessageBody);

            // assert
            ((string) topic).Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoTopicInMessageBody_WhenCreatingTopic_ThenItShouldThrow()
        {
            // arrange
            EventData eventData = new EventData(Encoding.UTF8.GetBytes("{'Dammit':'Bobby'}"));
            EventMessageBody eventMessageBody = new EventMessageBody(eventData);

            // act
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => { string s =  new Topic(eventMessageBody); };

            // assert
            action.Should().Throw<Exception>();
        }
    }
}