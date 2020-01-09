using FluentAssertions;
using GoogazonActivities.TopicNeedActivity;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using TestConveniences;

namespace GoogazonActivities.Unit.Tests.TopicNeedActivity
{
    [TestClass]
    public class TopicNeedActivityTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenNeed_WhenAskingForToExpressNeed_ThenItShouldCallExpressNeed()
        {
            // arrange
            FakeEventMessage fakeEventMessage = new FakeEventMessage(new EventData(new ArraySegment<byte>()));
            FakeNeed fakeNeed = new FakeNeed();
            const string stricklandPropaneLlc = "Strickland Propane, LLC";
            ExpandoObject expected = JsonConvert.DeserializeObject<ExpandoObject>($"{{'BusinessName':'{stricklandPropaneLlc}'}}");
            FakeResult fakeResult = new FakeResult(expected);
            INeedActivity needActivity = new Privateer().Object<GoogazonActivities.TopicNeedActivity.TopicNeedActivity>(fakeEventMessage, fakeNeed, fakeResult);   

            // act
            Func<Task> func = async () => await needActivity.ExpressNeed();
            func.Invoke();

            // assert
            fakeNeed.CallCountSpy.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenFulfilledNeed_WhenAskingForValue_ThenItShouldReturnCorrectValue()
        {
            // arrange
            FakeEventMessage fakeEventMessage = new FakeEventMessage(new EventData(new ArraySegment<byte>()));
            FakeNeed fakeNeed = new FakeNeed();
            const string stricklandPropaneLlc = "Strickland Propane, LLC";
            ExpandoObject expected = JsonConvert.DeserializeObject<ExpandoObject>($"{{'BusinessName':'{stricklandPropaneLlc}'}}");
            FakeResult fakeResult = new FakeResult(expected);
            INeedActivity needActivity = new Privateer().Object<GoogazonActivities.TopicNeedActivity.TopicNeedActivity>(fakeEventMessage, fakeNeed, fakeResult);

            // act
            dynamic actual = needActivity.ValueAsync().Result;

            // assert
            ((ExpandoObject) actual).Should().BeEquivalentTo(expected);
            ((string) actual.BusinessName).Should().Be(stricklandPropaneLlc);
        }
    }
}