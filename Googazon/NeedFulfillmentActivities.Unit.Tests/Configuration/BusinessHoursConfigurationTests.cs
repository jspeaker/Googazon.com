using FluentAssertions;
using Googazon.Library.Exceptions;
using Googazon.Library.PrimitiveConcepts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillmentActivities.Configuration;
using NeedFulfillmentActivities.Models.BusinessHours;
using System;
using System.Collections.Generic;

namespace NeedFulfillmentActivities.Unit.Tests.Configuration
{
    [TestClass]
    public class BusinessHoursConfigurationTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenValidHoursJson_WhenAskingForHours_ThenItShouldReturnHydratedListOfObjects()
        {
            // arrange
            const string json = "[{'DayOfWeek': 'Sunday','OpenTime': '10:00:00','CloseTime': '16:00:00'},{'DayOfWeek': 'Monday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Tuesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Wednesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Thursday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Friday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Saturday','OpenTime': '8:00:00','CloseTime': '17:00:00'}]";
            Environment.SetEnvironmentVariable(new FakeConfigKey(), json);
            IBusinessHoursConfiguration businessHoursConfiguration = new BusinessHoursConfiguration();

            // act
            List<Hours> hours = businessHoursConfiguration.Hours(new FakeConfigKey());

            // assert
            hours.Should().HaveCount(7);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoValidHoursJson_WhenAskingForHours_ThenItShouldThrow()
        {
            // arrange
            const string json = null;
            Environment.SetEnvironmentVariable(new FakeConfigKey(), json);
            IBusinessHoursConfiguration businessHoursConfiguration = new BusinessHoursConfiguration();

            // act
            Action action = () => businessHoursConfiguration.Hours(new FakeConfigKey());

            // assert
            action.Should().Throw<ConfigurationItemNotFoundException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEmptySetOfHoursJson_WhenAskingForHours_ThenItShouldThrow()
        {
            // arrange
            const string json = "[]";
            Environment.SetEnvironmentVariable(new FakeConfigKey(), json);
            IBusinessHoursConfiguration businessHoursConfiguration = new BusinessHoursConfiguration();

            // act
            Action action = () => businessHoursConfiguration.Hours(new FakeConfigKey());

            // assert
            action.Should().Throw<ConfigurationItemNotValidException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenInvalidHoursJson_WhenAskingForHours_ThenItShouldThrow()
        {
            // arrange
            const string json = "{}";
            Environment.SetEnvironmentVariable(new FakeConfigKey(), json);
            IBusinessHoursConfiguration businessHoursConfiguration = new BusinessHoursConfiguration();

            // act
            Action action = () => businessHoursConfiguration.Hours(new FakeConfigKey());

            // assert
            action.Should().Throw<Exception>();
        }
    }

    public class FakeConfigKey : Text
    {
        public FakeConfigKey() : base("key") { }
    }
}