using System;
using MerakiDashboard;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Configuration settings for a <see cref="MerakiDashboardClient"/>.
    /// </summary>
    public class MerakiDashboardClientSettings
    {
        /// <summary>
        /// The scheme and host name portion of the URL, e.g. "https://dashboard.meraki.com".
        /// </summary>
        public Uri BaseAddress { get; set; }

        /// <summary>
        /// The API Key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}