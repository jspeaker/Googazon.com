using FluentAssertions;
using Googazon.Library.Configuration;
using Googazon.Library.Exceptions;
using Googazon.Library.Texts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Googazon.Library.Unit.Tests.Configuration
{
    [TestClass]
    public class EventHubConfigurationTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionStringInConfiguration_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "connection-string";
            Environment.SetEnvironmentVariable(new EventHubConnectionStringKey(), expected);
            IEventHubConfiguration eventHubConfiguration = new EventHubConfiguration();

            // act
            string actual = eventHubConfiguration.ConnectionString();

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionStringNotInConfiguration_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            Environment.SetEnvironmentVariable(new EventHubConnectionStringKey(), null);
            Environment.GetEnvironmentVariables().Clear();
            IEventHubConfiguration eventHubConfiguration = new EventHubConfiguration();

            // act
            Action action = () => eventHubConfiguration.ConnectionString();

            // assert
            action.Should().Throw<ConfigurationItemNotFoundException>();
        }
    }
}