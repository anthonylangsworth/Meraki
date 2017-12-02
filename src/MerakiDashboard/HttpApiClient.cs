using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MerakiDashboard
{
    /// <summary>
    /// Call HTTP apis.
    /// </summary>
    internal class HttpApiClient
    {
        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

        /// <summary>
        /// Create a new <see cref="HttpApiClient"/>.
        /// </summary>
        /// <param name="baseAddress">
        /// The base URI for web service calls. This must be absolute and cannot be null.
        /// </param>
        /// <param name="httpRequestHeaders">
        /// An optional parameter containing HTTP headers to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseAddress"/> cannot be null.
        /// </exception>
        public HttpApiClient(Uri baseAddress, HttpRequestHeaders httpRequestHeaders = null)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException("Must be absolute URI", nameof(baseAddress));
            }

            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(baseAddress.AbsoluteUri, UriKind.Absolute)
            };
            if (httpRequestHeaders != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in httpRequestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Format and escape the given string for a URI.
        /// </summary>
        /// <param name="formattable">
        /// The string to format.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public string Url(FormattableString formattable)
        {
            return formattable.ToString(_formatter);
        }

        /// <summary>
        /// Call the given URL asynchronously.
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
            using (HttpResponseMessage response = await _client.SendAsync(new HttpRequestMessage(method, uri)))
            {
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
            using (HttpResponseMessage response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
