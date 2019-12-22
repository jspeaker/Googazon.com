using System;
using System.Dynamic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillmentActivities.Models;
using Newtonsoft.Json;

namespace NeedFulfillmentActivities.Unit.Tests.Models
{
    [TestClass]
    public class CallCenterStateTests
    {
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
            ((string) dynamicStateObject.sourceOperation).Should().Be("CustomerServiceCallCenterOpenFunction");
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
            ((string) dynamicStateObject.sourceOperation).Should().Be("CustomerServiceCallCenterOpenFunction");
            ((string) dynamicStateObject.sourceAssembly).Should().StartWith("NeedFulfillment");
            ((DateTime) dynamicStateObject.timestamp).Should().BeAfter(DateTime.UtcNow.AddMilliseconds(-100));
        }
    }

    public class FakeCallCenterOpen : ICallCenterOpen
    {
        private readonly bool _open;

        public FakeCallCenterOpen(bool open) => _open = open;

        public bool IsOpen() => _open;
    }
}