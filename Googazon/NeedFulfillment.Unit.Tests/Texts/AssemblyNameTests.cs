using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyName = NeedFulfillment.Texts.AssemblyName;

namespace NeedFulfillment.Unit.Tests.Texts
{
    [TestClass]
    public class AssemblyNameTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenAssemblyName_WhenUsingImplicitOperator_ThenItShouldReturnCorrectString()
        {
            // arrange
            const string expected = "NeedFulfillment, Version=1";
            AssemblyName assemblyName = new AssemblyName();

            // act
            string actual = assemblyName;

            // assert
            actual.Should().StartWith(expected);
        }
    }
}