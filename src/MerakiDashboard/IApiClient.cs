using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MerakiDashboard
{
    /// <summary>
    /// Generic API client.
    /// </summary>
    internal interface IApiClient: IDisposable
    {
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
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri);

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
        Task<T> GetAsync<T>(string uri);

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
        Task<string> GetAsync(string uri);

        /// <summary>
        /// Base address used for calls.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        /// The Meraki Dashboard API key.
        /// </summary>
        string ApiKey { get; }
    }
}
