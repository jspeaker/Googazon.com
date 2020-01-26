using FluentAssertions;
using GoogazonActivities.Messaging.Strategies;
using GoogazonActivities.Texts.ConfigurationKeys;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;
using TestConveniences;

namespace GoogazonActivities.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusTopicPostmanTests
    {
        [TestMethod, TestCategory("Unit")]
        public async Task GivenTopic_WhenAskingToSend_ThenItShouldCallTopicClient()
        {
            // arrange
            FakeTopicClient fakeTopicClient = new FakeTopicClient();
            Message message = new Message(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakePostman fakePostman = new FakePostman();
            IServiceBusPostman postman = new Privateer().Object<ServiceBusTopicPostman>(fakeTopicClient, message, fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakeTopicClient.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public async Task GivenTopic_WhenAskingToSend_ThenItShouldCallNextStrategy()
        {
            // arrange
            FakeTopicClient fakeTopicClient = new FakeTopicClient();
            Message message = new Message(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakePostman fakePostman = new FakePostman();
            IServiceBusPostman postman = new Privateer().Object<ServiceBusTopicPostman>(fakeTopicClient, message, fakePostman);

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            fakePostman.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoTopic_WhenInstantiating_ThenItShouldThrow()
        {
            // arrange
            Environment.SetEnvironmentVariable(new ServiceBusConnectionStringKey(), "connection-string");
            // act
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new ServiceBusTopicPostman(string.Empty, string.Empty, "{}", new FakePostman());

            // assert
            action.Should().Throw<ArgumentException>();
        }
    }
}