using FluentAssertions;
using GoogazonFunctions.Needs;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;
using GoogazonFunctions.Unit.Tests.Fakes;

namespace GoogazonFunctions.Unit.Tests.Needs
{
    [TestClass]
    public class NeedsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenEventMessage_WhenAskingToSendNeed_ThenItShouldReturnEventData()
        {
            // arrange
            EventData expected = new EventData(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakeEventMessage fakeEventMessage = new FakeEventMessage(expected);
            Need need = new Need(fakeEventMessage, new FakeEventHubClient());

            // act
            EventData actual = need.SendAsync().Result;

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEventMessage_WhenAskingToSendNeed_ThenItShouldCallEventHubClient()
        {
            // arrange
            EventData expected = new EventData(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakeEventMessage fakeEventMessage = new FakeEventMessage(expected);
            FakeEventHubClient fakeEventHubClient = new FakeEventHubClient();
            Need need = new Need(fakeEventMessage, fakeEventHubClient);

            // act
            Func<Task> func = async () => await need.SendAsync();
            func.Invoke();

            // assert
            fakeEventHubClient.CallCount.Should().Be(1);
        }
    }
}