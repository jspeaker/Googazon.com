using FluentAssertions;
using Googazon.Library.PrimitiveConcepts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Googazon.Library.Unit.Tests.PrimitiveConcepts
{
    [TestClass]
    public class TextTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenFakeText_WhenUsingImplicitOperator_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string expected = "Dammit, Bobby!";
            FakeText fakeText = new FakeText(expected);
            
            // act
            string actual = fakeText;

            // assert
            actual.Should().Be(expected);
        }
    }

    public class FakeText : Text
    {
        public FakeText(string value) : base(value) { }
    }
}