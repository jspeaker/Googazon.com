using FluentAssertions;
using GoogazonFunctions.Texts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonFunctions.Unit.Tests.Texts
{
    [TestClass]
    public class CallCenterOpenNeedTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenCallCenterOpenNeed_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "callcenteropen";
            CallCenterOpenNeed callCenterOpenNeed = new CallCenterOpenNeed();

            // act
            string actual = callCenterOpenNeed;

            // assert
            actual.Should().Be(expected);
        }
    }
}