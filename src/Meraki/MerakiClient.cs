using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meraki {
    public partial class MerakiClient {
        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

        public MerakiClient(IOptions<MerakiClientSettings> options) {
            _client = new HttpClient(new HttpClientHandler()) {
                BaseAddress = new Uri(options.Value.Address)
            };
            _client.DefaultRequestHeaders.Add("X-Cisco-Meraki-API-Key", options.Value.Key);
            _client.DefaultRequestHeaders.Add("Accept-Type", "application/json");
        }

        private string Url(FormattableString formattable) => formattable.ToString(_formatter);

        public Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri) {
            return SendAsync(new HttpRequestMessage(method, uri));
        }

        internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
            return _client.SendAsync(request);
        }

        internal async Task<T> GetAsync<T>(string uri) {
            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> GetAsync(string uri) {
            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}