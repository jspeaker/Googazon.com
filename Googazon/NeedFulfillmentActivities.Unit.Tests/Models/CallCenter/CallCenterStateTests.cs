using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillmentActivities.Models;
using NeedFulfillmentActivities.Models.CallCenter;
using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace NeedFulfillmentActivities.Unit.Tests.Models.CallCenter
{
    [TestClass]
    public class CallCenterStateTests
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable(new CallCenterHoursKey(), "[{'DayOfWeek': 'Sunday','OpenTime': '10:00:00','CloseTime': '16:00:00'},{'DayOfWeek': 'Monday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Tuesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Wednesday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Thursday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Friday','OpenTime': '6:00:00','CloseTime': '18:00:00'},{'DayOfWeek': 'Saturday','OpenTime': '8:00:00','CloseTime': '17:00:00'}]");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenOpenCallCenter_WhenInstantiating_ThenItShouldReturnOpenStateHydratedObject()
        {
            // arrange
            // act
            CallCenterState callCenterState = new CallCenterState(new FakeCallCenterOpen(true));

            // assert
            string stateObject = JsonConvert.SerializeObject(callCenterState);
            dynamic dynamicStateObject = JsonConvert.DeserializeObject<ExpandoObject>(stateObject);
            ((bool) dynamicStateObject.open).Should().BeTrue();
            ((string) dynamicStateObject.sourceOperation).Should().Be("Call Center");
            ((string) dynamicStateObject.sourceAssembly).Should().StartWith("NeedFulfillment");
            ((DateTime) dynamicStateObject.timestamp).Should().BeAfter(DateTime.UtcNow.AddMilliseconds(-500));
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenClosedCallCenter_WhenInstantiating_ThenItShouldReturnClosedStateHydratedObject()
        {
            // arrange
            // act
            CallCenterState callCenterState = new CallCenterState(new FakeCallCenterOpen(false));

            // assert
            string stateObject = JsonConvert.SerializeObject(callCenterState);
            dynamic dynamicStateObject = JsonConvert.DeserializeObject<ExpandoObject>(stateObject);
            ((bool) dynamicStateObject.open).Should().BeFalse();
            ((string) dynamicStateObject.sourceOperation).Should().Be("Call Center");
            ((string) dynamicStateObject.sourceAssembly).Should().StartWith("NeedFulfillment");
            ((DateTime) dynamicStateObject.timestamp).Should().BeAfter(DateTime.UtcNow.AddMilliseconds(-500));
        }
    }

    public class FakeCallCenterOpen : IContactMethodOpen
    {
        private readonly bool _open;

        public FakeCallCenterOpen(bool open) => _open = open;

        public bool IsOpen() => _open;
        public NeedFulfillmentActivities.Models.BusinessHours.OpenHours OpenHours() => new NeedFulfillmentActivities.Models.BusinessHours.OpenHours(new CallCenterHoursKey());
    }
}