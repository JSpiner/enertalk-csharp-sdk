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
            _token = await SendWebRequest<Token>(HttpMethod.Post, url, value);
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
            _token = await SendWebRequest<Token>(HttpMethod.Post, url, value);
            _token.TokenType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_token.TokenType);

            IsAuthorized = true;
        }

        public async Task<Device[]> GetDevices()
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string url = "https://api.encoredtech.com/1.2/devices/list";
            return await SendWebRequest<Device[]>(HttpMethod.Get, url);
        }

        public async Task<PlugRelayStatus> GetPlugRelayStatusAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/relay";
            string url = string.Format(template, deviceId);
            return await SendWebRequest<PlugRelayStatus>(HttpMethod.Get, url);
        }

        public async Task<PlugRelayStatus> TogglePlugRelayStatusAsync(string deviceId, string mode)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/relay?mode={1}";
            string url = string.Format(template, deviceId);
            return await SendWebRequest<PlugRelayStatus>(HttpMethod.Put, url, mode);
        }

        public async Task<RealtimeUsage> GetRealtimeUsageAsync(string deviceId)
        {
            if (!IsAuthorized)
                throw new InvalidOperationException();

            string template = "https://api.encoredtech.com/1.2/devices/{0}/realtimeUsage";
            string url = string.Format(template, deviceId);
            return await SendWebRequest<RealtimeUsage>(HttpMethod.Get, url);
        }

        private async Task<T> SendWebRequest<T>(HttpMethod method, string url, object value = null)
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