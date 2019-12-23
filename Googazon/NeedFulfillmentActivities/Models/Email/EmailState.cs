using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.Email
{
    public class EmailState
    {
        private readonly IContactMethodOpen _contactMethodOpen;

        public EmailState() : this(new EmailOpen()) { }

        public EmailState(IContactMethodOpen chatOpen) => _contactMethodOpen = chatOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _contactMethodOpen.IsOpen();

        [JsonProperty("hours")] private BusinessHours.OpenHours Hours => _contactMethodOpen.OpenHours();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Email";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}