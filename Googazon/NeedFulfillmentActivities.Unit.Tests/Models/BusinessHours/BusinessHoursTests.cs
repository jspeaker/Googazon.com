using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillmentActivities.Models.BusinessHours;
using NeedFulfillmentActivities.Texts;
using System;

namespace NeedFulfillmentActivities.Unit.Tests.Models.BusinessHours
{
    [TestClass]
    public class BusinessHoursTests
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable(new CallCenterHoursKey(), "[{'DayOfWeek': 'Sunday','OpenTime': '10:00:00','CloseTime': '16:00:00'},{'DayOfWeek': 'Monday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Tuesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Wednesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Thursday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Friday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Saturday','OpenTime': '8:00:00','CloseTime': '17:00:00'}]");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenBeforeBottomEdgeOnSunday_ThenItShouldReturnFalse()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime sunday = new DateTime(2018, 1, 7, 9, 59, 59);

            // act
            bool actual = openHours.IsOpen(sunday);

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAtBottomEdgeOnSunday_ThenItShouldReturnTrue()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime sunday = new DateTime(2018, 1, 7, 10, 00, 00);

            // act
            bool actual = openHours.IsOpen(sunday);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAtTopEdgeOnSunday_ThenItShouldReturnTrue()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime sunday = new DateTime(2018, 1, 7, 16, 00, 00);

            // act
            bool actual = openHours.IsOpen(sunday);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAfterTopEdgeOnSunday_ThenItShouldReturnTrue()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime sunday = new DateTime(2018, 1, 7, 16, 00, 01);

            // act
            bool actual = openHours.IsOpen(sunday);

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenBeforeBottomEdgeOnSaturday_ThenItShouldReturnFalse()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime saturday = new DateTime(2018, 1, 6, 07, 59, 59);

            // act
            bool actual = openHours.IsOpen(saturday);

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAtBottomEdgeOnSaturday_ThenItShouldReturnTrue()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime saturday = new DateTime(2018, 1, 6, 08, 00, 00);

            // act
            bool actual = openHours.IsOpen(saturday);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAtTopEdgeOnSaturday_ThenItShouldReturnTrue()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime saturday = new DateTime(2018, 1, 6, 17, 00, 00);

            // act
            bool actual = openHours.IsOpen(saturday);

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSpecificBusinessHoursInConfiguration_WhenAskingIsOpenAfterTopEdgeOnSaturday_ThenItShouldReturnFalse()
        {
            // arrange
            IOpenHours openHours = new OpenHours(new CallCenterHoursKey());
            DateTime saturday = new DateTime(2018, 1, 6, 17, 00, 01);

            // act
            bool actual = openHours.IsOpen(saturday);

            // assert
            actual.Should().BeFalse();
        }
    }
}