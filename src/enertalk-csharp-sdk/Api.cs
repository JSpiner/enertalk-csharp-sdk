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
        private string _clientId;

        private string _secretId;

        private Token _token;
        
        public Api(string clientId, string secretId)
        {
            _clientId = clientId;
            _secretId = secretId;
        }

        public bool IsAuthorized { get; private set; }

        public async Task AuthorizeAsync(string code)
        {
            var value = new
            {
                client_id = _clientId,
                client_secret = _secretId,
                grant_type = "authorization_code",
                code = code,
            };
            
            string url = "https://enertalk-auth.encoredtech.com/token";
            _token = await SendWebRequest<Token>(HttpMethod.Post, url, value);
            _token.TokenType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_token.TokenType);
            IsAuthorized = true;
        }

        private async Task<T> SendWebRequest<T>(HttpMethod method, string url, object value)
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