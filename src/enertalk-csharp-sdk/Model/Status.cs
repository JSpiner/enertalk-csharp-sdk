using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class Status
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
