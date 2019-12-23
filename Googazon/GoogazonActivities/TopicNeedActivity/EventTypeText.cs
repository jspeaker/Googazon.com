using System;
using GoogazonActivities.Models;

namespace GoogazonActivities.TopicNeedActivity
{
    public class EventTypeText
    {
        private readonly string _eventType;
        public EventTypeText(string eventType) => _eventType = eventType;

        public EventType AsEnum()
        {
            if (!Enum.TryParse(_eventType, true, out EventType eventType)) return EventType.None;
            return eventType;
        }
    }
}