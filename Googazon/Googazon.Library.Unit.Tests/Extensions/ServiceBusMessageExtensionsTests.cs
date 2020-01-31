using FluentAssertions;
using Googazon.Library.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Googazon.Library.Unit.Tests.Extensions
{
    [TestClass]
    public class ServiceBusMessageExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenServiceBusMessage_WhenAskingBodyAsString_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string messageBody = "message body";
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // act
            string actual = message.BodyAsString();

            // assert
            actual.Should().Be(messageBody);
        }
    }
}