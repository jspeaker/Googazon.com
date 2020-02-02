using FluentAssertions;
using GoogazonActivities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogazonActivities.Unit.Tests.Models
{
    [TestClass]
    public class ResourceBasedNeedTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenResourceBasedNeed_WhenUsingImplicitOperator_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string resource = "resource";
            const string need = "need";
            ResourceBasedNeed resourceBasedNeed = new ResourceBasedNeed(resource, need);

            // act
            string actual = resourceBasedNeed;

            // assert
            actual.Should().Be($"{resource}{need}");
        }
    }
}