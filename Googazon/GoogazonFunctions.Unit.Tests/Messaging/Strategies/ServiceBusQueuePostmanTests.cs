﻿using FluentAssertions;
using GoogazonFunctions.Messaging.Strategies;
using GoogazonFunctions.Unit.Tests.Fakes;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GoogazonFunctions.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusQueuePostmanTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenUnenrichedMessage_WhenAskingToSend_ThenItShouldOnlyCallNextStrategy()
        {
            // arrange
            const string messageBody = "{'Dammit':'Bobby!'}";
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            FakeQueueClient fakeQueueClient = new FakeQueueClient();
            FakePostman fakePostman = new FakePostman();
            ServiceBusQueuePostman postman = new ServiceBusQueuePostman(fakeQueueClient, messageBody, message, fakePostman);

            // act
            Func<Task> func = async () => await postman.SendAsync();
            func.Invoke();

            // assert
            fakeQueueClient.CallCount.Should().Be(0);
            fakePostman.CallCount.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessage_WhenAskingToSend_ThenItShouldOnlyCallQueueClient()
        {
            // arrange
            const string messageBody = "{'Dammit':'Bobby!','Results':'Dirty hippies.'}";
            Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
            FakeQueueClient fakeQueueClient = new FakeQueueClient();
            FakePostman fakePostman = new FakePostman();
            ServiceBusQueuePostman postman = new ServiceBusQueuePostman(fakeQueueClient, messageBody, message, fakePostman);

            // act
            Func<Task> func = async () => await postman.SendAsync();
            func.Invoke();

            // assert
            fakeQueueClient.CallCount.Should().Be(1);
            fakePostman.CallCount.Should().Be(0);
        }
    }
}