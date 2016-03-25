using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class RealtimeUsage
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("voltage")]
        public long Voltage { get; set; }

        [JsonProperty("current")]
        public long Current { get; set; }

        [JsonProperty("activePower")]
        public long ActivePower { get; set; }

        [JsonProperty("apparentPower")]
        public long ApparentPower { get; set; }

        [JsonProperty("reactivePower")]
        public long ReactivePower { get; set; }

        [JsonProperty("powerFactor")]
        public long PowerFactor { get; set; }

        [JsonProperty("wattHour")]
        public long WattHour { get; set; }

        [JsonProperty("powerBase")]
        public long PowerBase { get; set; }
    }
}
