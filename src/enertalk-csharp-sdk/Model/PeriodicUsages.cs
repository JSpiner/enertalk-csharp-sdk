using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class PeriodicUsages
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("unitPeriodUsage")]
        public long UnitPeriodUsage { get; set; }

        [JsonProperty("unitPeriodBill")]
        public long UnitPeriodBill { get; set; }
    }
}
