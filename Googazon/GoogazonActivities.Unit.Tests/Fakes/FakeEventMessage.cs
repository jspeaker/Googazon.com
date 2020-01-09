using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;
using System;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeEventMessage : IEventMessage
    {
        private readonly EventData _eventData;

        public FakeEventMessage(EventData eventData) => _eventData = eventData;

        public EventData EventData() => _eventData;

        public bool IsEventType(EventType eventType) => true;

        public string UniqueIdentifier() => Guid.Empty.ToString();

        public string Topic() => "topic";

        public string Need() => "need";
    }
}