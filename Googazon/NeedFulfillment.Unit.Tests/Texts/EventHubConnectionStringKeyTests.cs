using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillment.Texts;

namespace NeedFulfillment.Unit.Tests.Texts
{
    [TestClass]
    public class EventHubConnectionStringKeyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenRapidsKey_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "EventHubConnectionString";
            EventHubConnectionStringKey key = new EventHubConnectionStringKey();

            // act
            string actual = key;

            // assert
            actual.Should().Be(expected);
        }
    }
}