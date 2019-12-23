using NeedFulfillmentActivities.Models.Chat;
using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.Email
{
    public class EmailState
    {
        private readonly IContactMethodOpen _callCenterOpen;

        public EmailState() : this(new ChatOpen()) { }

        public EmailState(IContactMethodOpen chatOpen) => _callCenterOpen = chatOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("hours")] private string Hours => _callCenterOpen.Hours();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Email";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}