using FluentAssertions;
using Googazon.Library.Exceptions;
using GoogazonActivities.Configuration;
using GoogazonActivities.Texts;
using GoogazonActivities.Texts.ConfigurationKeys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoogazonActivities.Unit.Tests.Configuration
{
    [TestClass]
    public class ResultConfigurationTests
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey(), null);
            Environment.SetEnvironmentVariable(new UniqueResultPollingFrequencyKey(), null);
            Environment.GetEnvironmentVariables().Clear();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenPollingFrequency_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            Environment.SetEnvironmentVariable(new UniqueResultPollingFrequencyKey(), "234");
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            TimeSpan actual = resultConfiguration.PollingFrequency();

            // assert
            actual.TotalMilliseconds.Should().Be(234);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenPollingFrequencyNotParseable_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            Environment.SetEnvironmentVariable(new UniqueResultPollingFrequencyKey(), "Dammit, Bobby!");
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            Action action = () => resultConfiguration.PollingFrequency();

            // assert
            action.Should().Throw<ConfigurationItemParsingException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoPollingFrequencyInConfiguration_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            Action action = () => resultConfiguration.PollingFrequency();

            // assert
            action.Should().Throw<ConfigurationItemNotFoundException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenResultTimeout_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            Environment.SetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey(), "123");
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            TimeSpan actual = resultConfiguration.UniqueResultTimeout();

            // assert
            actual.TotalMilliseconds.Should().Be(123);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenResultTimeoutNotParseable_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            Environment.SetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey(), "Dammit, Bobby!");
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            Action action = () => resultConfiguration.UniqueResultTimeout();

            // assert
            action.Should().Throw<ConfigurationItemParsingException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoResultTimeoutInConfiguration_WhenAskingForValue_ThenItShouldThrow()
        {
            // arrange
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            Action action = () => resultConfiguration.UniqueResultTimeout();

            // assert
            action.Should().Throw<ConfigurationItemNotFoundException>();
        }
    }
}