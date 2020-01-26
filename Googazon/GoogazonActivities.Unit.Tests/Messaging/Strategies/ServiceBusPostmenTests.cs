using FluentAssertions;
using GoogazonActivities.Messaging.Strategies;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TestConveniences;

namespace GoogazonActivities.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusPostmenTests
    {
        [TestMethod, TestCategory("Unit")]
        public async Task GivenPostmanChainThatShouldAllExecute_WhenAskingToSend_ThenItShouldExecuteCorrectPostmen()
        {
            // arrange
            Privateer privateer = new Privateer();
            FakePostman nextFakePostman = new FakePostman(true);
            FakePostman fakePostman = new FakePostman(true, nextFakePostman);
            IServiceBusPostman postman = privateer.Object<ServiceBusPostmen>(fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakePostman.CallCount.Should().Be(1);
            nextFakePostman.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public async Task GivenPostmanChainThatShouldNotAllExecute_WhenAskingToSend_ThenItShouldExecuteCorrectPostmen()
        {
            // arrange
            Privateer privateer = new Privateer();
            FakePostman nextFakePostman = new FakePostman(true);
            FakePostman fakePostman = new FakePostman(false, nextFakePostman);
            IServiceBusPostman postman = privateer.Object<ServiceBusPostmen>(fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakePostman.CallCount.Should().Be(0);
            nextFakePostman.CallCount.Should().Be(1);
        }
    }
}