﻿using FluentAssertions;
using GoogazonActivities.Messaging;
using GoogazonActivities.Unit.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TestConveniences;

namespace GoogazonActivities.Unit.Tests.Messaging
{
    [TestClass]
    public class ServiceBusMessageTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenAnySituation_WhenAskingToSend_ThenItShouldCallPostman()
        {
            // arrange
            FakePostman fakePostman = new FakePostman();
            ServiceBusMessage serviceBusMessage = new Privateer().Object<ServiceBusMessage>(fakePostman);

            // act
            Func<Task> func = async () => await serviceBusMessage.SendAsync();
            func.Invoke();

            // assert
            fakePostman.CallCount.Should().Be(1);
        }
    }
}