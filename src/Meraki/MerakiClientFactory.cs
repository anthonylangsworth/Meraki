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
            // var options = Options.Create(settings);
            var setup = new MerakiClientSettingsSetup();

            setup.Configure(settings);
            configure?.Invoke(settings);

            return new MerakiClient(settings);
        }
    }
}