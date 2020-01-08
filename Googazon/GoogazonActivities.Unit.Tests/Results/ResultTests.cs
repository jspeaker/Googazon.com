using FluentAssertions;
using GoogazonActivities.Caching;
using GoogazonActivities.Results;
using GoogazonActivities.Texts;
using GoogazonActivities.Texts.ConfigurationKeys;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Dynamic;
using System.Net;

namespace GoogazonActivities.Unit.Tests.Results
{
    [TestClass]
    public class ResultTests
    {
        private const string Id = "id";

        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey(), "500");
            Environment.SetEnvironmentVariable(new UniqueResultPollingFrequencyKey(), "10");
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