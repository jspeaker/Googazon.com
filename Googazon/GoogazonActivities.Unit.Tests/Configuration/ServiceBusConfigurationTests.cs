using FluentAssertions;
using GoogazonActivities.Configuration;
using GoogazonActivities.Texts.ConfigurationKeys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoogazonActivities.Unit.Tests.Configuration
{
    [TestClass]
    public class ServiceBusConfigurationTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenServiceBusConnectionStringInConfiguration_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "connection-string";
            Environment.SetEnvironmentVariable(new ServiceBusConnectionStringKey(), expected);
            IServiceBusConfiguration serviceBusConfiguration = new ServiceBusConfiguration();

            // act
            string actual = serviceBusConfiguration.ConnectionString();

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenServiceBusConnectionStringNotInConfiguration_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            Environment.SetEnvironmentVariable(new ServiceBusConnectionStringKey(), null);
            Environment.GetEnvironmentVariables().Clear();
            IServiceBusConfiguration serviceBusConfiguration = new ServiceBusConfiguration();

            // act
            Action action = () => serviceBusConfiguration.ConnectionString();

            // assert
            action.Should().Throw<Exception>();
        }
    }
}