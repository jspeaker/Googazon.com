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

        [JsonProperty("hours")] private BusinessHours.OpenHours Hours => _callCenterOpen.OpenHours();

        [JsonProperty("headquartersAddress")] private HeadquartersAddress HeadquartersAddress => new HeadquartersAddress();

        [JsonProperty("sourceOperation")] private readonly string _sourceOperation = "Googazon Store";

        [JsonProperty("sourceAssembly")] private readonly string _sourceAssembly = new AssemblyName();

        [JsonProperty("timestamp")] private readonly DateTime _enrichmentDateTime = DateTime.UtcNow;
#pragma warning restore 414
    }

    public class HeadquartersAddress
    {
        [JsonProperty("streetAddress1")] private readonly string _streetAddress1 = "1 Googazon Blvd.";
        [JsonProperty("streetAddress2")] private readonly string _streetAddress2 = "";
        [JsonProperty("city")] private readonly string _city = "Philipsburg";
        [JsonProperty("state")] private readonly string _state = "Montana";
        [JsonProperty("zipCode")] private readonly string _zipCode = "59858-0001";
    }
}