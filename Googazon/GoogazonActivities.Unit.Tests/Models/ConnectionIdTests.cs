using FluentAssertions;
using GoogazonActivities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GoogazonActivities.Unit.Tests.Models
{
    [TestClass]
    public class ConnectionIdTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionIdInHeaders_WhenAskingForConnectionId_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string connectionId = "connection-id";
            IHeaderDictionary headerDictionary = new HeaderDictionary(new Dictionary<string, StringValues>
            {
                { "X-ConnectionId", new StringValues(connectionId) }
            });

            // act
            string actual = new ConnectionId(headerDictionary);

            // assert
            actual.Should().Be(connectionId);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionIdWithFunkyCasingInHeaders_WhenAskingForConnectionId_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string connectionId = "connection-id";
            IHeaderDictionary headerDictionary = new HeaderDictionary(new Dictionary<string, StringValues>
            {
                { "X-cOnnECtIONId", new StringValues(connectionId) }
            });

            // act
            string actual = new ConnectionId(headerDictionary);

            // assert
            actual.Should().Be(connectionId);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenConnectionIdNotInHeaders_WhenAskingForConnectionId_ThenItShouldReturnEmptyString()
        {
            // arrange
            const string connectionId = "connection-id";
            IHeaderDictionary headerDictionary = new HeaderDictionary(new Dictionary<string, StringValues>
            {
                { "X-DammitBobby", new StringValues(connectionId) }
            });

            // act
            string actual = new ConnectionId(headerDictionary);

            // assert
            actual.Should().Be(string.Empty);
        }
    }
}