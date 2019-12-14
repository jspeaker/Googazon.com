using NeedFulfillment.Functions;
using NeedFulfillment.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillment.Models
{
    public class CallCenterState
    {
        private readonly ICallCenterOpen _callCenterOpen;

        public CallCenterState() : this(new CallCenterOpen()) { }

        public CallCenterState(ICallCenterOpen callCenterOpen) => _callCenterOpen = callCenterOpen;

        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = nameof(CustomerServiceCallCenterOpenFunction);

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
    }
}