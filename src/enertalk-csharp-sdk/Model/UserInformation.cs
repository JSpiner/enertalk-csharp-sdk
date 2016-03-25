using System;
using Newtonsoft.Json;

namespace Enertalk.Model
{
    public class UserInformation
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("agreementSMS")]
        public bool AgreementSMS { get; set; }

        [JsonProperty("agreementEmail")]
        public bool AgreementEmail { get; set; }

        [JsonProperty("meteringDay")]
        public int MeteringDay { get; set; }

        [JsonProperty("contractType")]
        public int ContractType { get; set; }

        [JsonProperty("contractPower")]
        public long? ContractPower { get; set; }

        [JsonProperty("maxLimitUsage")]
        public long? MaxLimitUsage { get; set; }

        [JsonProperty("needUpdate")]
        public bool NeedUpdate { get; set; }
    }
}
