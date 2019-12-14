using System;
using NeedFulfillment.Functions;
using NeedFulfillment.Texts;
using Newtonsoft.Json;

namespace NeedFulfillment.Models
{
    internal class CallCenterState
    {
        public CallCenterState() : this(new CallCenterOpen()) { }

        private CallCenterState(bool open) => _open = open;

        [JsonProperty("source.operation")] private readonly string _sourceOperation = nameof(CustomerServiceCallCenterOpenFunction);

        [JsonProperty("source.assembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("open")] private bool _open;

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;

        private class CallCenterOpen
        {
            private static bool IsOpen() => DateTime.UtcNow.Hour >= 6 && DateTime.UtcNow.Hour <= 18;

            public static implicit operator bool(CallCenterOpen instance) => IsOpen();
        }
    }
}