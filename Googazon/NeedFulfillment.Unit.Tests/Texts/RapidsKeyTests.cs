using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillment.Texts;

namespace NeedFulfillment.Unit.Tests.Texts
{
    [TestClass]
    public class RapidsKeyTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenRapidsKey_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "rapids";
            RapidsKey rapidsKey = new RapidsKey();

            // act
            string actual = rapidsKey;

            // assert
            actual.Should().Be(expected);
        }
    }
}