using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MerakiDashboard
{
    /// <summary>
    /// Call HTTP apis.
    /// </summary>
    internal sealed class HttpApiClient: IApiClient
    {
        private const string AcceptTypeHttpHeader = "Accept-Type";
        private const string MerakiApiKeyHttpHeader = "X-Cisco-Meraki-API-Key";

        /// <summary>
        /// Create a new <see cref="HttpApiClient"/>.
        /// </summary>
        /// <param name="baseAddress">
        /// The base URI for web service calls. This must be absolute and cannot be null.
        /// </param>
        /// <param name="apiKey">
        /// An optional parameter containing HTTP headers to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseAddress"/> cannot be null.
        /// </exception>
        public HttpApiClient(Uri baseAddress, string apiKey)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException("Must be absolute URI", nameof(baseAddress));
            }
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            HttpClient = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(baseAddress.AbsoluteUri, UriKind.Absolute)
            };
            HttpClient.DefaultRequestHeaders.Add(MerakiApiKeyHttpHeader, apiKey);
            HttpClient.DefaultRequestHeaders.Add(AcceptTypeHttpHeader, "application/json");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Class is sealed so no need for common IDisposable pattern
            HttpClient?.Dispose();
        }

        /// <summary>
        /// Base address used for calls.
        /// </summary>
        public Uri BaseAddress => HttpClient.BaseAddress;

        /// <summary>
        /// The Meraki Dashboard API key.
        /// </summary>
        public string ApiKey => HttpClient.DefaultRequestHeaders.GetValues(MerakiApiKeyHttpHeader).FirstOrDefault();

        /// <summary>
        /// Actually does the calls.
        /// </summary>
        internal HttpClient HttpClient { get; }

        /// <summary>
        /// Call the given URL asynchronously.
        /// </summary>
        /// <param name="method">
        /// </param>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>. The caller is responsible for Disposing
        /// the returned object.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                HttpResponseMessage response = await HttpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        /// <summary>
        /// Call the given URL asynchronously using an HTTP GET method, expecting returned data.
        /// </summary>
        /// <typeparam name="T">
        /// The expected return type.
        /// </typeparam>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The result as a string.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<T> GetAsync<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(uri));
        }

        /// <summary>
        /// Call the given URL asynchronously using an HTTP GET method, expecting returned data.
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
            using (HttpResponseMessage response = await HttpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
