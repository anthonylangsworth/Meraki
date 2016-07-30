using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meraki {
    public partial class MerakiClient {
        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

        public MerakiClient(IOptions<MerakiClientSettings> options)
            : this(options.Value) {
        }

        public MerakiClient(MerakiClientSettings settings) {
            _client = new HttpClient(new HttpClientHandler()) {
                BaseAddress = new Uri(settings.Address)
            };
            _client.DefaultRequestHeaders.Add("X-Cisco-Meraki-API-Key", settings.Key);
            _client.DefaultRequestHeaders.Add("Accept-Type", "application/json");
        }

        private string Url(FormattableString formattable) => formattable.ToString(_formatter);

        public Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri) => SendAsync(new HttpRequestMessage(method, uri));

        internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => _client.SendAsync(request);

        internal async Task<T> GetAsync<T>(string uri) {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> GetAsync(string uri) {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}