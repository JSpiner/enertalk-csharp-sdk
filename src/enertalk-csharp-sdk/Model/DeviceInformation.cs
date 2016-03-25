using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class DeviceInformation
    {
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }
    }
}
