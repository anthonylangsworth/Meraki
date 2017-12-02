using System;
using Microsoft.Extensions.Options;

namespace MerakiDashboard
{
    /// <summary>
    /// Initialize a <see cref="MerakiDashboardClientSettings"/> object.
    /// </summary>
    internal class MerakiDashboardClientSettingsSetup : ConfigureOptions<MerakiDashboardClientSettings>
    {
        public static readonly string DefaultMerakiDashboardApiBaseAddress = "https://dashboard.meraki.com/";

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
            options.BaseAddress = new Uri(DefaultMerakiDashboardApiBaseAddress, UriKind.Absolute);
            options.ApiKey = "";
        }
    }
}