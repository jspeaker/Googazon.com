using FluentAssertions;
using Googazon.Library.Extensions;
using Microsoft.Azure.EventHubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Dynamic;

namespace Googazon.Library.Unit.Tests.Extensions
{
    [TestClass]
    public class DynamicExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenDynamic_WhenAskingSerialized_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string json = "{\"Dammit\":\"Bobby\"}";
            ExpandoObject expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json);

            // act
            string actual = expandoObject.Serialized();

            // assert
            actual.Should().Be(json);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenDynamic_WhenAskingAsEventData_ThenItShouldReturnCorrectValue()
        {
            // arrange
            const string json = "{\"Dammit\":\"Bobby\"}";
            ExpandoObject expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json);

            // act
            EventData actual = expandoObject.AsEventData();

            // assert
            actual.Should().BeOfType<EventData>();
        }
    }
}