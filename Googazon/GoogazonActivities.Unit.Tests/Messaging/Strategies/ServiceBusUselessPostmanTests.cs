using FluentAssertions;
using GoogazonActivities.Messaging.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace GoogazonActivities.Unit.Tests.Messaging.Strategies
{
    [TestClass]
    public class ServiceBusUselessPostmanTests
    {
        [TestMethod, TestCategory("Unit")]
        public async Task GivenAnySituation_WhenAskingToSend_ThenItShouldCompleteSuccessfully()
        {
            // arrange
            ServiceBusUselessPostman postman = new ServiceBusUselessPostman();

            // act
            async Task func() => await postman.SendAsync();
            await func();

            // assert
            func().IsCompletedSuccessfully.Should().BeTrue();
        }
    }
}