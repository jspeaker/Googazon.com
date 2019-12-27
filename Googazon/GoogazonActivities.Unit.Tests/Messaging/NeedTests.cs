using FluentAssertions;
using GoogazonActivities.Messaging;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace GoogazonActivities.Unit.Tests.Messaging
{
    [TestClass]
    public class NeedTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenNeedInMessageBody_WhenCreatingTopic_ThenItShouldHydrateIt()
        {
            // arrange
            const string expected = "Propane Accessories";
            EventData eventData = new EventData(Encoding.UTF8.GetBytes($"{{'need':'{expected}'}}"));
            EventMessageBody eventMessageBody = new EventMessageBody(eventData);

            // act
            Need topic = new Need(eventMessageBody);

            // assert
            ((string) topic).Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoNeedInMessageBody_WhenCreatingTopic_ThenItShouldThrow()
        {
            // arrange
            EventData eventData = new EventData(Encoding.UTF8.GetBytes("{'Dammit':'Bobby'}"));
            EventMessageBody eventMessageBody = new EventMessageBody(eventData);

            // act
            // ReSharper disable once UnusedVariable
            Action action = () => { string s =  new Need(eventMessageBody); };

            // assert
            action.Should().Throw<Exception>();
        }
    }
}