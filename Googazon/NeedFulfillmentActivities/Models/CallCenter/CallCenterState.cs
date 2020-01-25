using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.CallCenter
{
    public class CallCenterState
    {
        private readonly IContactMethodOpen _callCenterOpen;

        public CallCenterState() : this(new CallCenterOpen()) { }

        public CallCenterState(IContactMethodOpen callCenterOpen) => _callCenterOpen = callCenterOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("hours")] private BusinessHours.OpenHours Hours => _callCenterOpen.OpenHours();

        [JsonProperty("phoneNumber")] private string PhoneNumber => "18662223333";

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Call Center";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}