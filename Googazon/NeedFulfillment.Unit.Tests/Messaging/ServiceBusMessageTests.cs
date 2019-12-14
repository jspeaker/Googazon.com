using FluentAssertions;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeedFulfillment.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace NeedFulfillment.Unit.Tests.Messaging
{
    [TestClass]
    public class ServiceBusMessageTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenInvalidJson_WhenUsingImplicitOperator_ThenItShouldThrow()
        {
            // arrange
            const string expected = "Dammit, Bobby!";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(expected);
            
            // ReSharper disable once NotAccessedVariable
            string actual;

            // act
            Action action = () => actual = serviceBusMessage;


            // assert
            action.Should().Throw<Exception>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenValidJson_WhenUsingImplicitOperator_ThenItShouldReturnRawJson()
        {
            // arrange
            const string expected = "{\"Dammit\":\"Bobby!\"}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(expected);

            // act
            string actual = serviceBusMessage;


            // assert
            actual.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenUnenrichedMessage_WhenAskingIsEnriched_ThenItShouldReturnFalse()
        {
            // arrange
            const string expected = "{\"Dammit\":\"Bobby!\"}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(expected);

            // act
            bool actual = serviceBusMessage.IsEnriched();

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessage_WhenAskingIsEnriched_ThenItShouldReturnTrue()
        {
            // arrange
            const string expected = "{\"Dammit\":\"Bobby!\",\"Results\":[{}]}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(expected);

            // act
            bool actual = serviceBusMessage.IsEnriched();

            // assert
            actual.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenUnenrichedMessage_WhenAskingForEnrichedInstance_ThenItShouldReturnNewObjectWithEnrichment()
        {
            // arrange
            const string json = "{\"Dammit\":\"Bobby!\"}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(json);
            dynamic enrichment = new ExpandoObject();

            // act
            ServiceBusMessage actual = serviceBusMessage.EnrichedInstance(enrichment);

            // assert
            actual.Should().NotBe(serviceBusMessage);
            actual.IsEnriched().Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessage_WhenAskingForEnrichedInstance_ThenItShouldReturnNewObjectWithOriginalAndNewEnrichment()
        {
            // arrange
            const string json = "{\"Dammit\":\"Bobby!\",\"Results\":[{\"Mother\":\"Will not be happy.\"}]}";
            const string expected = "{\"Dammit\":\"Bobby!\",\"Results\":[{\"Mother\":\"Will not be happy.\"},{\"Hello\":\", World.\"}]}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(json);
            Enrichment enrichment = new Enrichment();

            // act
            ServiceBusMessage actual = serviceBusMessage.EnrichedInstance(enrichment);

            // assert
            actual.Should().NotBe(serviceBusMessage);
            actual.IsEnriched().Should().BeTrue();
            ((string) actual).Should().Be(expected);
        }


        [TestMethod, TestCategory("Unit")]
        public void GivenEnrichedMessage_WhenAskingForItAsEventData_ThenItShouldReturnHydratedEventData()
        {
            // arrange
            const string expected = "{\"Dammit\":\"Bobby!\",\"Results\":[{}]}";
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(expected);

            // act
            EventData actual = serviceBusMessage.AsEventData();

            // assert
            actual.Body.Array?.Length.Should().BeGreaterThan(0);
            dynamic actualObject = JsonConvert.DeserializeObject<ExpandoObject>(Encoding.UTF8.GetString(actual.Body.Array));
            ((string) actualObject.Dammit).Should().Be("Bobby!");
            ((List<object>) actualObject.Results).Should().BeOfType<List<object>>();
            ((List<object>) actualObject.Results).First().Should().BeOfType<ExpandoObject>();
        }
        
        private class Enrichment
        {
            // ReSharper disable once UnusedMember.Local
            public string Hello => ", World.";
        }
    }
}