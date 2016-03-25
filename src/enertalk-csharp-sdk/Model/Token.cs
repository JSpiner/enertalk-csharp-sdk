using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class Token
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiredAt { get; set; }
    }
}
