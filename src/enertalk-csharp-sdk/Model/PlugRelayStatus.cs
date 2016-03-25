using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class PlugRelayStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
