using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.BrickAndMortar
{
    public class BrickAndMortarState
    {
        private readonly IContactMethodOpen _callCenterOpen;

        public BrickAndMortarState() : this(new BrickAndMortarOpen()) { }

        public BrickAndMortarState(IContactMethodOpen contactMethodOpen) => _callCenterOpen = contactMethodOpen;

#pragma warning disable 414
        [JsonProperty("open")] private bool Open => _callCenterOpen.IsOpen();

        [JsonProperty("hours")] private string Hours => _callCenterOpen.Hours();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Googazon Store";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }
}