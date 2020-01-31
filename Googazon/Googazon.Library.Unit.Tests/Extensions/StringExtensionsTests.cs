using FluentAssertions;
using Googazon.Library.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Googazon.Library.Unit.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenString_WhenAskingAsBytes_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string messageBody = "message body";

            // act
            byte[] actual = messageBody.AsBytes();

            // assert
            actual.Should().BeOfType<byte[]>();
            Encoding.UTF8.GetString(actual).Should().Be(messageBody);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenString_WhenAskingAsServiceBusMessage_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string messageBody = "message body";

            // act
            Message actual = messageBody.AsServiceBusMessage();

            // assert
            actual.Should().BeOfType<Message>();
            Encoding.UTF8.GetString(actual.Body).Should().Be(messageBody);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenString_WhenAskingAsServiceBusMessageWithUserProperties_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string messageBody = "message body";

            // act
            StringExtensionsTests expectedOne = new StringExtensionsTests();
            StringExtensionsTests expectedTwo = new StringExtensionsTests();
            Message actual = messageBody.AsServiceBusMessage(new List<UserProperty>{
                new UserProperty("Dammit", expectedOne),
                new UserProperty("Bobby", expectedTwo)
            });

            // assert
            actual.Should().BeOfType<Message>();
            actual.UserProperties.Should().HaveCount(2);
            actual.UserProperties["Dammit"].Should().Be(expectedOne);
            actual.UserProperties["Bobby"].Should().Be(expectedTwo);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWellFormedJson_WhenAskingAsTypeExpandoObject_ThenItShouldReturnCorrectValue()
        {
            // arrange
            string messageBody = $"{{'Dammit':'Bobby'}}";

            // act
            ExpandoObject expandoObject = messageBody.AsType<ExpandoObject>();

            // assert
            expandoObject.Should().BeOfType<ExpandoObject>();
            ((string) ((dynamic) expandoObject).Dammit).Should().Be("Bobby");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenMalformedJson_WhenAskingAsTypeExpandoObject_ThenItShouldThrow()
        {
            // arrange
            string messageBody = $"{{'Dammit, Bobby!'}}";

            // act
            Action action = () => messageBody.AsType<ExpandoObject>();

            // assert
            action.Should().Throw<Exception>();
        }
    }
}