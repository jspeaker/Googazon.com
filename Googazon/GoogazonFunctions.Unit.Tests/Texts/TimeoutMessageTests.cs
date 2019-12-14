using FluentAssertions;
using GoogazonFunctions.Texts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonFunctions.Unit.Tests.Texts
{
    [TestClass]
    public class TimeoutMessageTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenResultsKey_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "The result did not become available within the timeout period.";
            TimeoutMessage timeoutMessage = new TimeoutMessage();

            // act
            string actual = timeoutMessage;

            // assert
            actual.Should().Be(expected);
        }
    }
}