using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Enertalk.Model;

namespace Enertalk
{
    public class Api
    {
        private string _secretId;

        private Token _token;
        
        public Api(string clientId, string secretId)
        {
            ClientId = clientId;
            _secretId = secretId;
        }

        public string ClientId { get; private set; }

        public bool IsAuthorized { get; private set; }

        public async Task AuthorizeAsync(string code)
        {
            IsAuthorized = false;
            _token = null;

            var value = new
            {
                client_id = ClientId,
                client_secret = _secretId,
                grant_type = "authorization_code",
                code = code,
            };
            
            string url = "https://enertalk-auth.encoredtech.com/token";
            _token = await SendWebRequestAsync<Token>(HttpMethod.Post, url, value);
            _token.TokenType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_token.TokenType);

            IsAuthorized = true;
        }

        public async Task RefreshToken()
        {
            IsAuthorized = false;

            var value = new
            {
                grant_type = "refresh_token",
                refresh_token = _token.RefreshToken,
            };

            _token = null;

            string url = "https://enertalk-auth.encoredtech.com/refresh";
            _token = await SendWebRequestAsync<Token>(HttpMethod.Post, url, value);
            _token.TokenType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_token.TokenType);

            IsAuthorized = true;
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

        private async Task SendWebRequestAsync(HttpMethod method, string url, object value = null)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = true,
            };

            using (var client = new HttpClient(handler))
            {
                var formatter = new JsonMediaTypeFormatter();
                var request = new HttpRequestMessage(method, url);
                if (value != null)
                    if (value is string)
                        request.Content = new StringContent((string)value);
                    else
                        request.Content = new ObjectContent(value.GetType(), value, formatter);

                if (IsAuthorized)
                {
                    request.Headers.Add("Authorization", string.Format("{0} {1}", _token.TokenType, _token.AccessToken));
                }

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<T> SendWebRequestAsync<T>(HttpMethod method, string url, object value = null)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy("http://127.0.0.1:8888"),
                UseProxy = true,
            };

            using (var client = new HttpClient(handler))
            {
                var formatter = new JsonMediaTypeFormatter();
                var request = new HttpRequestMessage(method, url);
                if (value != null)
                    if (value is string)
                        request.Content = new StringContent((string)value);
                    else
                        request.Content = new ObjectContent(value.GetType(), value, formatter);

                if (IsAuthorized)
                {
                    request.Headers.Add("Authorization", string.Format("{0} {1}", _token.TokenType, _token.AccessToken));
                }
                
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<T>();
            }
        }

    }
}