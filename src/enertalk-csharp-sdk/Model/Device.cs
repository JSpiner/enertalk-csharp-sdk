using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class Device
    {
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("createdAt")]
        public int CreatedAt { get; set; }

        [JsonProperty("uuid")]
        public string UUID { get; set; }
    }
}
