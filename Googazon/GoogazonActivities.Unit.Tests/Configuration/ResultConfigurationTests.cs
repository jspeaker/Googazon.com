using FluentAssertions;
using Googazon.Library.Exceptions;
using GoogazonActivities.Configuration;
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
            Environment.SetEnvironmentVariable("UniqueResultTimeoutMilliseconds", null);
            Environment.GetEnvironmentVariables().Remove("UniqueResultTimeoutMilliseconds");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenResultTimeout_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            Environment.SetEnvironmentVariable("UniqueResultTimeoutMilliseconds", "123");
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
            Environment.SetEnvironmentVariable("UniqueResultTimeoutMilliseconds", "Dammit, Bobby!");
            IResultConfiguration resultConfiguration = new ResultConfiguration();

            // act
            Action action = () => resultConfiguration.UniqueResultTimeout();

            // assert
            action.Should().Throw<ConfigurationItemNotFoundException>();
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