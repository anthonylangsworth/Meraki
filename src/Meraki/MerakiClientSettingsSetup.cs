using Microsoft.Extensions.Options;

namespace Meraki
{
    /// <summary>
    /// Initialize a <see cref="MerakiClientSettings"/> object.
    /// </summary>
    public class MerakiClientSettingsSetup : ConfigureOptions<MerakiClientSettings>
    {
        /// <summary>
        /// Create a new <see cref="MerakiClientSettingsSetup"/> object.
        /// </summary>
        public MerakiClientSettingsSetup() : base(ConfigureOptions)
        {
            // Do nothing
        }

        /// <summary>
        /// Configure the <see cref="MerakiClientSettings"/> object.
        /// </summary>
        /// <param name="options">
        /// The <see cref="MerakiClientSettings"/> object to configure.
        /// </param>
        private static void ConfigureOptions(MerakiClientSettings options)
        {
            options.Address = "https://dashboard.meraki.com";
            options.Key = "";
        }
    }
}