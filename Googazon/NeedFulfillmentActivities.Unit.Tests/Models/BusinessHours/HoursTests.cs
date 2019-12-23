using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillmentActivities.Models.BusinessHours;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Unit.Tests.Models.BusinessHours
{
    [TestClass]
    public class HoursTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenSundayClosedHours_WhenAskingIsOpenSundayAtOpenCloseEdge_ThenItShouldReturnFalse()
        {
            // arrange
            const string json = "{'DayOfWeek':'Sunday','OpenTime':'00:00:00','CloseTime':'00:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Sunday, TimeSpan.Parse("00:00:00"));

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSaturdayHours_WhenAskingIsOpenSaturdayBeforeBottomEdge_ThenItShouldReturnFalse()
        {
            // arrange
            const string json = "{'DayOfWeek':'Saturday','OpenTime':'06:00:00','CloseTime':'18:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Saturday, TimeSpan.Parse("05:59:59"));

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSaturdayHours_WhenAskingIsOpenSaturdayAtBottomEdge_ThenItShouldReturnTrue()
        {
            // arrange
            const string json = "{'DayOfWeek':'Saturday','OpenTime':'06:00:00','CloseTime':'18:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Saturday, TimeSpan.Parse("06:00:00"));

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSaturdayHours_WhenAskingIsOpenSaturdayAtTopEdge_ThenItShouldReturnTrue()
        {
            // arrange
            const string json = "{'DayOfWeek':'Saturday','OpenTime':'06:00:00','CloseTime':'18:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Saturday, TimeSpan.Parse("18:00:00"));

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSaturdayHours_WhenAskingIsOpenSaturdayAfterTopEdge_ThenItShouldReturnFalse()
        {
            // arrange
            const string json = "{'DayOfWeek':'Saturday','OpenTime':'06:00:00','CloseTime':'18:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Saturday, TimeSpan.Parse("18:00:01"));

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenSaturdayHours_WhenAskingIsOpenSundayWithinSaturdayHours_ThenItShouldReturnFalse()
        {
            // arrange
            const string json = "{'DayOfWeek':'Saturday','OpenTime':'06:00:00','CloseTime':'18:00:00'}";
            IHours hours = JsonConvert.DeserializeObject<Hours>(json);

            // act
            bool actual = hours.IsOpen(DayOfWeek.Sunday, TimeSpan.Parse("12:00:00"));

            // assert
            actual.Should().BeFalse();
        }
    }
}