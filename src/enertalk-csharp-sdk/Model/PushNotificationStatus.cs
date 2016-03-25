using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class PushNotificationStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
