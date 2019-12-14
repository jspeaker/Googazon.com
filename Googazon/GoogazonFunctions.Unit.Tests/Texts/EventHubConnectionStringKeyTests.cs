using FluentAssertions;
using GoogazonFunctions.Texts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonFunctions.Unit.Tests.Texts
{
    [TestClass]
    public class EventHubConnectionStringKeyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenEventHubConnectionStringKey_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
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