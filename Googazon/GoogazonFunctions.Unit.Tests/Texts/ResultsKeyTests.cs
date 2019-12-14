using FluentAssertions;
using GoogazonFunctions.Texts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonFunctions.Unit.Tests.Texts
{
    [TestClass]
    public class ResultsKeyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenResultsKey_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "Results";
            ResultsKey resultsKey = new ResultsKey();

            // act
            string actual = resultsKey;

            // assert
            actual.Should().Be(expected);
        }
    }
}