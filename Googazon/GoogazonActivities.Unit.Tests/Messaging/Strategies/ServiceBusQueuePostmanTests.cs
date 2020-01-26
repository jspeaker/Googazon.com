using FluentAssertions;
using GoogazonActivities.Messaging.Strategies;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;
using TestConveniences;

namespace GoogazonActivities.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusQueuePostmanTests
    {
        [TestMethod, TestCategory("Unit")]
        public async Task GivenUnenrichedMessage_WhenAskingToSend_ThenItShouldOnlyCallNextStrategy()
        {
            // arrange
            const string messageBody = "{'Dammit':'Bobby!'}";
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            FakeQueueClient fakeQueueClient = new FakeQueueClient();
            FakePostman fakePostman = new FakePostman();
            ServiceBusQueuePostman postman = new Privateer().Object<ServiceBusQueuePostman>(fakeQueueClient, messageBody, message, fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakeQueueClient.CallCount.Should().Be(0);
            fakePostman.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public async Task GivenEnrichedMessage_WhenAskingToSend_ThenItShouldOnlyCallQueueClient()
        {
            // arrange
            const string messageBody = "{'Dammit':'Bobby!','Results':'Dirty hippies.'}";
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            FakeQueueClient fakeQueueClient = new FakeQueueClient();
            FakePostman fakePostman = new FakePostman();
            ServiceBusQueuePostman postman = new Privateer().Object<ServiceBusQueuePostman>(fakeQueueClient, messageBody, message, fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakeQueueClient.CallCount.Should().Be(1);
            fakePostman.CallCount.Should().Be(0);
        }
    }
}