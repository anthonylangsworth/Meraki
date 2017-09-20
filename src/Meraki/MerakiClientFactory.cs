using System;
using Microsoft.Extensions.Options;

namespace Meraki
{
    /// <summary>
    /// Factory to create <see cref="MerakiClient"/> objects.
    /// </summary>
    public static class MerakiClientFactory
    {
        /// <summary>
        /// Create a <see cref="MerakiClient"/> based off the given <see cref="MerakiClientSettings"/> object.
        /// </summary>
        /// <param name="configure">
        /// An optional configuration step on the <see cref="MerakiClientSettings"/> object being created.
        /// </param>
        /// <returns>
        /// A configured <see cref="MerakiClient"/> object.
        /// </returns>
        public static MerakiClient Create(Action<MerakiClientSettings> configure = null)
        {
            var settings = new MerakiClientSettings();
            var setup = new MerakiClientSettingsSetup();

            setup.Configure(settings);
            configure?.Invoke(settings);

            return new MerakiClient(settings);
        }

        /// <summary>
        /// Create a <see cref="MerakiClient"/> using the given <paramref name="apiKey"/>.
        /// </summary>
        /// <param name="apiKey">
        /// The API Key to use. This cannot be null, empty or whitespace.
        /// </param>
        /// <returns>
        /// A configured <see cref="MerakiClient"/> object.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="apiKey"/> cannot be null, empty or whitespace.
        /// </exception>
        public static MerakiClient Create(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));   
            }

            return Create(mcs => mcs.Key = apiKey);
        }
    }
}