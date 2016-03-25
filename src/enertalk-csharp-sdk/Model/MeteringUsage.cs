using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class MeteringUsage
    {
        [JsonProperty("meteringStart")]
        public long MeteringStart { get; set; }

        [JsonProperty("meteringEnd")]
        public long MeteringEnd { get; set; }

        [JsonProperty("meteringPeriodUsage")]
        public long MeteringPeriodUsage { get; set; }

        [JsonProperty("meteringPeriodBill")]
        public long MeteringPeriodBill { get; set; }
    }
}
