using System;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GoogazonActivities.Messaging.Strategies;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonActivities.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusTopicPostmanTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenTopic_WhenAskingToSend_ThenItShouldCallTopicClient()
        {
            // arrange
            Message message = new Message(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakeTopicClient fakeTopicClient = new FakeTopicClient();
            IServiceBusPostman postman = new ServiceBusTopicPostman(fakeTopicClient, message, new FakePostman());

            // act
            Func<Task> func = async () => await postman.SendAsync();
            func.Invoke();

            // assert
            fakeTopicClient.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenTopic_WhenAskingToSend_ThenItShouldCallNextStrategy()
        {
            // arrange
            Message message = new Message(Encoding.UTF8.GetBytes("{'Dammit':'Bobby!'}"));
            FakePostman fakePostman = new FakePostman();
            IServiceBusPostman postman = new ServiceBusTopicPostman(new FakeTopicClient(), message, fakePostman);

            // act
            Func<Task> func = async () => await postman.SendAsync();
            func.Invoke();

            // assert
            fakePostman.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenNoTopic_WhenInstantiating_ThenItShouldThrow()
        {
            // arrange
            // act
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new ServiceBusTopicPostman(string.Empty, string.Empty, "{}", new FakePostman());

            // assert
            action.Should().Throw<ArgumentException>();
        }
    }
}