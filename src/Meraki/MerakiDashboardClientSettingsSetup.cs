using System;
using Microsoft.Extensions.Options;

namespace Meraki
{
    /// <summary>
    /// Initialize a <see cref="MerakiDashboardClientSettings"/> object.
    /// </summary>
    internal class MerakiDashboardClientSettingsSetup : ConfigureOptions<MerakiDashboardClientSettings>
    {
        /// <summary>
        /// Create a new <see cref="MerakiDashboardClientSettingsSetup"/> object.
        /// </summary>
        public MerakiDashboardClientSettingsSetup() : base(ConfigureOptions)
        {
            // Do nothing
        }

        /// <summary>
        /// Configure the <see cref="MerakiDashboardClientSettings"/> object.
        /// </summary>
        /// <param name="options">
        /// The <see cref="MerakiDashboardClientSettings"/> object to configure.
        /// </param>
        private static void ConfigureOptions(MerakiDashboardClientSettings options)
        {
            options.Address = new Uri("https://dashboard.meraki.com", UriKind.Absolute);
            options.Key = "";
        }
    }
}