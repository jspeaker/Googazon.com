using System;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeEventMessage : IEventMessage
    {
        private readonly EventData _eventData;

        public FakeEventMessage(EventData eventData) => _eventData = eventData;

        public EventData EventData() => _eventData;

        public bool IsEventType(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public string UniqueIdentifier()
        {
            throw new NotImplementedException();
        }

        public string Topic()
        {
            throw new NotImplementedException();
        }

        public string Need()
        {
            throw new NotImplementedException();
        }
    }
}