using System;
using System.Net.Http;
using System.Threading.Tasks;
using Enertalk.Model;

namespace Enertalk
{
    public class PlugApi : BaseApi
    {
        public PlugApi(string clientId, string secretId)
            : base(clientId, secretId)
        {
        }
        
        public async Task<Device[]> GetDevices()
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string url = "https://api.encoredtech.com/1.2/devices/list";
            return await SendWebRequestAsync<Device[]>(HttpMethod.Get, url);
        }

        public async Task<PlugRelayStatus> GetPlugRelayStatusAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/relay";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<PlugRelayStatus>(HttpMethod.Get, url);
        }

        public async Task<PlugRelayStatus> TogglePlugRelayStatusAsync(string deviceId, string mode)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/relay?mode={1}";
            string url = string.Format(template, deviceId, mode);
            return await SendWebRequestAsync<PlugRelayStatus>(HttpMethod.Put, url, mode);
        }

        public async Task<RealtimeUsage> GetRealtimeUsageAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/realtimeUsage";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<RealtimeUsage>(HttpMethod.Get, url);
        }
    }
}