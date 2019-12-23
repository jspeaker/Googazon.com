using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.Chat
{
    public class ChatState
    {
        private readonly IContactMethodOpen _callCenterOpen;

        public ChatState() : this(new ChatOpen()) { }

        public ChatState(IContactMethodOpen chatOpen) => _callCenterOpen = chatOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("hours")] private BusinessHours.OpenHours Hours => _callCenterOpen.OpenHours();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Live Chat";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}