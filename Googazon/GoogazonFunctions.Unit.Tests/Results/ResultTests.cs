using FluentAssertions;
using GoogazonFunctions.Results;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Dynamic;
using System.Net;

namespace GoogazonFunctions.Unit.Tests.Results
{
    [TestClass]
    public class ResultTests
    {
        private const string Id = "id";

        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("UniqueResultTimeoutMilliseconds", "100");
            InMemoryCache.Instance().Remove(Id);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEmptyCache_WhenAskingForResultItem_ThenItShouldThrow()
        {
            // arrange
            Result result = new Result();

            // act
            Action action = () => result.Item(Id);

            // assert
            action.Should().Throw<WebException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenUnenrichedCacheItem_WhenAskingForResultItem_ThenItShouldThrow()
        {
            // arrange
            InMemoryCache.Instance().Set(Id, new ExpandoObject());
            Result result = new Result();

            // act
            Action action = () => result.Item(Id);

            // assert
            action.Should().Throw<WebException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedCacheItem_WhenAskingForResultItem_ThenItReturnHydratedObject()
        {
            // arrange
            dynamic expandoObject = new ExpandoObject();
            expandoObject.Results = new { Property = "value" };
            InMemoryCache.Instance().Set(Id, (ExpandoObject) expandoObject);
            Result result = new Result();

            // act
            dynamic actual = result.Item(Id);

            // assert
            ((string) actual.Results.Property).Should().Be("value");
        }
    }
}