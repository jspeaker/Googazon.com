using FluentAssertions;
using GoogazonFunctions.Messaging;
using GoogazonFunctions.Texts;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoogazonFunctions.Unit.Tests.Messaging
{
    [TestClass]
    public class ServiceBusTopicTests
    {
        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable(new CustomerServiceRiverConnectionStringKey(), "Endpoint=sb://googazon-customerservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=asdf3tSTcS3NdZ+xPyvkhCgFIeed+f/P8WcSw+pxx5w=");
            new Privateer().Field<Dictionary<string, ITopicClient>>(new ServiceBusTopic(), "TopicClients").Clear();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenTopic_WhenAskingForClient_ThenItReturnsCorrectClient()
        {
            // arrange
            // act
            const string expected = "topic";
            ITopicClient topicClient = ServiceBusTopic.Client(expected);

            // assert
            topicClient.TopicName.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenTopic_WhenAskingForClientTwice_ThenItReturnsTheSameObject()
        {
            // arrange
            // act
            const string topic = "topic";
            ITopicClient topicClient = ServiceBusTopic.Client(topic);
            ITopicClient anotherTopicClient = ServiceBusTopic.Client(topic);

            // assert
            topicClient.Should().Be(anotherTopicClient);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenTopics_WhenAskingForDifferentTopicClients_ThenItReturnDistinctObjects()
        {
            // arrange
            // act
            const string topic1 = "topic1";
            const string topic2 = "topic2";
            ITopicClient topicClient1 = ServiceBusTopic.Client(topic1);
            ITopicClient topicClient2 = ServiceBusTopic.Client(topic2);

            // assert
            topicClient1.Should().NotBe(topicClient2);
        }
    }

    public class Privateer
    {
        public T Object<T>(params object[] parameters) where T : class
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, parameters.Select(p => p.GetType()).ToArray(), null);
            if (constructor == null) throw new NullReferenceException($"Private constructor for type '{nameof(T)}' with the specified parameters was not found.");

            return (T) constructor.Invoke(parameters);
        }

        public T Field<T>(object instance, string fieldName)
        {
            FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            return (T) field.GetValue(instance);
        }

        public void SetField<T>(object instance, string fieldName, T value)
        {
            FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            field.SetValue(instance, value);
        }
    }
}