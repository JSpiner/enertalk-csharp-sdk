using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class ForcastUsage
    {
        [JsonProperty("meteringDay")]
        public int MeteringDay { get; set; }

        [JsonProperty("meteringStart")]
        public long MeteringStart { get; set; }

        [JsonProperty("meteringEnd")]
        public long MeteringEnd { get; set; }

        [JsonProperty("meteringPeriodUsage")]
        public long MeteringPeriodUsage { get; set; }
    }
}
