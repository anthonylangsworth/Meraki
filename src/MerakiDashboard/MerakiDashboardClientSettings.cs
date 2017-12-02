using System;

namespace MerakiDashboard
{
    /// <summary>
    /// Configuration settings for a <see cref="MerakiDashboardClient"/>.
    /// </summary>
    public class MerakiDashboardClientSettings
    {
        /// <summary>
        /// The scheme and host name portion of the URL, e.g. "https://dashboard.meraki.com".
        /// </summary>
        public Uri Address { get; set; }

        /// <summary>
        /// The API Key.
        /// </summary>
        public string Key { get; set; }
    }
}