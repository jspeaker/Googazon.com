using System;
using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;

namespace NeedFulfillmentActivities.Models
{
    public class CallCenterState
    {
        private readonly ICallCenterOpen _callCenterOpen;

        public CallCenterState() : this(new CallCenterOpen()) { }

        public CallCenterState(ICallCenterOpen callCenterOpen) => _callCenterOpen = callCenterOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "CustomerServiceCallCenterOpenFunction";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}