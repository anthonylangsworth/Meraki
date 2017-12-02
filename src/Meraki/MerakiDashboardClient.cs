using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meraki
{
    /// <summary>
    /// Wrapper around Meraki APIs.
    /// </summary>
    public partial class MerakiDashboardClient
    {
        private const string AcceptTypeHttpHeader = "Accept-Type";
        private const string MerakiApiKeyHttpHeader = "X-Cisco-Meraki-API-Key";
        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

        /// <summary>
        /// Create a new <see cref="MerakiDashboardClient"/>.
        /// </summary>
        /// <param name="options">
        /// The options to use. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="options"/> cannot be null.
        /// </exception>
        public MerakiDashboardClient(IOptions<MerakiDashboardClientSettings> options)
            : this(options?.Value)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="MerakiDashboardClient"/>.
        /// </summary>
        /// <param name="settings">
        /// The <see cref="MerakiDashboardClientSettings"/> to use. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="settings"/> nor <paramref name="settings.Address"/> can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="MerakiDashboardClientSettings.Key"/> field cannot be null, empty or whitespace.
        /// </exception>
        public MerakiDashboardClient(MerakiDashboardClientSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (settings.Address == null)
            {
                throw new ArgumentException("Missing address", nameof(settings));
            }
            if (string.IsNullOrWhiteSpace(settings.Key))
            {
                throw new ArgumentException("Missing API key", nameof(settings));
            }

            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(settings.Address.AbsoluteUri)
            };
            _client.DefaultRequestHeaders.Add(MerakiApiKeyHttpHeader, settings.Key);
            _client.DefaultRequestHeaders.Add(AcceptTypeHttpHeader, "application/json");
        }

        /// <summary>
        /// Base address for Meraki API calls.
        /// </summary>
        public Uri Address => _client.BaseAddress;

        /// <summary>
        /// The Meraki API key used.
        /// </summary>
        public string ApiKey => _client.DefaultRequestHeaders.GetValues(MerakiApiKeyHttpHeader).FirstOrDefault();

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
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri)
        {
            using (HttpResponseMessage response = await SendAsync(new HttpRequestMessage(method, uri)))
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
        } 

        internal async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (HttpResponseMessage response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        internal async Task<T> GetAsync<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(uri));
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
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<string> GetAsync(string uri)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
            using (HttpResponseMessage response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}