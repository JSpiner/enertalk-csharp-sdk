using System;
using System.Net.Http;
using System.Threading.Tasks;
using Enertalk.Model;

namespace Enertalk
{
    public class HomeApi : BaseApi
    {
        public HomeApi(string clientId, string secretId)
            : base(clientId, secretId)
        {
        }

        public async Task<Device> GetDeviceAsync()
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string url = "https://enertalk-auth.encoredtech.com/uuid";
            return await SendWebRequestAsync<Device>(HttpMethod.Get, url);
        }

        public async Task<PushNotificationStatus> TogglePushServiceAsync(string deviceId, bool enabled)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            var value = new
            {
                status = enabled.ToString().ToLower(),
            };

            string template = "https://api.encoredtech.com/1.2/devices/{0}/events/push";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<PushNotificationStatus>(HttpMethod.Put, url, value);
        }

        public async Task<PushNotificationStatus> GetPushNotificationSettingAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();
            
            string template = "https://api.encoredtech.com/1.2/devices/{0}/events/push";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<PushNotificationStatus>(HttpMethod.Get, url);
        }

        public async Task<PushNotificationStatus> RegisterPushIdAsync(string deviceId, bool registerId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            var value = new
            {
                type = "AND",
                regId = registerId
            };

            string template = "https://api.encoredtech.com/1.2/devices/{0}/events/push";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<PushNotificationStatus>(HttpMethod.Post, url, value);
        }

        public async Task<ForcastUsage> GetDeviceForecastUsageAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/forecastUsage";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<ForcastUsage>(HttpMethod.Get, url);
        }

        public async Task<DeviceInformation> GetDeviceInformationAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<DeviceInformation>(HttpMethod.Get, url);
        }

        public async Task<MeteringUsage> GetDeviceMeteringUsageAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/meteringUsage";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<MeteringUsage>(HttpMethod.Get, url);
        }

        public async Task<MeteringUsage[]> GetDeviceMeteringUsagesAsync(string deviceId, DateTime startDateTime, DateTime endDateTime)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            var value = string.Format("start={0}&end={1}", startDateTime.ToUnixTime(), endDateTime.ToUnixTime());

            string template = "https://api.encoredtech.com/1.2/devices/{0}/meteringUsages";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<MeteringUsage[]>(HttpMethod.Get, url, value);
        }

        public async Task<PeriodicUsages[]> GetDevicePeriodicUsagesAsync(string deviceId, DateTime startDateTime, DateTime endDateTime)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            var value = string.Format("period={0}&start={1}&end={2}", "hourly", startDateTime.ToUnixTime(), endDateTime.ToUnixTime());
            
            string template = "https://api.encoredtech.com/1.2/devices/{0}/usages";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<PeriodicUsages[]>(HttpMethod.Get, url, value);
        }

        public async Task<RealtimeUsage> GetRealtimeUsagesAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();
            
            string template = "https://api.encoredtech.com/1.2/devices/{0}/realtimeUsage";
            string url = string.Format(template, deviceId);
            return await SendWebRequestAsync<RealtimeUsage>(HttpMethod.Get, url);
        }

        public async Task<UserInformation> GetUserInformationAsync()
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/me";
            string url = string.Format(template);
            return await SendWebRequestAsync<UserInformation>(HttpMethod.Get, url);
        }

        public async Task UpdateUserInformationAsync(string deviceId, UserInformation userInformation)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/me";
            string url = string.Format(template);
            await SendWebRequestAsync(HttpMethod.Put, url, userInformation);
        }
    }
}