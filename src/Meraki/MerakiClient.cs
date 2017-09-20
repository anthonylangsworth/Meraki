using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meraki {
    public partial class MerakiClient {
        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

        /// <summary>
        /// Create a new <see cref="MerakiClient"/>.
        /// </summary>
        /// <param name="options">
        /// The options to use. This cannot be null.
        /// </param>
        public MerakiClient(IOptions<MerakiClientSettings> options)
            : this(options?.Value)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="MerakiClient"/>.
        /// </summary>
        /// <param name="settings">
        /// The <see cref="MerakiClientSettings"/> to use. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="settings"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="MerakiClientSettings.Address"/> or <see cref="MerakiClientSettings.Key"/> fields
        /// cannot be null, empty or whitespace.
        /// </exception>
        public MerakiClient(MerakiClientSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (string.IsNullOrWhiteSpace(settings.Address))
            {
                throw new ArgumentException("Missing address", nameof(settings));
            }
            if (string.IsNullOrWhiteSpace(settings.Key))
            {
                throw new ArgumentException("Missing API key", nameof(settings));
            }

            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(settings.Address)
            };
            _client.DefaultRequestHeaders.Add("X-Cisco-Meraki-API-Key", settings.Key);
            _client.DefaultRequestHeaders.Add("Accept-Type", "application/json");
        }

        private string Url(FormattableString formattable) => formattable.ToString(_formatter);

        /// <summary>
        /// Call the given URL asynchronously, not expecting any returned data.
        /// </summary>
        /// <param name="method">
        /// </param>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>.
        /// </returns>
        public Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri) => SendAsync(new HttpRequestMessage(method, uri));

        internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => _client.SendAsync(request);

        internal async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Call the given URL asynchronously, expecting returned data.
        /// </summary>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The result as a string.
        /// </returns>
        public async Task<string> GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}