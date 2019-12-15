using FluentAssertions;
using GoogazonFunctions.Messaging;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace GoogazonFunctions.Unit.Tests.Messaging
{
    [TestClass]
    public class EventMessageBodyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenValidBody_WhenUsingImplicitOperator_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "{'Dammit':'Bobby!'}";
            EventMessageBody eventMessageBody = new EventMessageBody(new EventData(Encoding.UTF8.GetBytes(expected)));

            // act
            string actual = eventMessageBody;

            // assert
            actual.Should().Be(expected);
        }
    }
}