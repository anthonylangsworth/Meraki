using Microsoft.Extensions.Options;

namespace MerakiDashboard.Test
{
    class MerakiDashboardClientSettingsOptions: IOptions<MerakiDashboardClientSettings>
    {
        /// <summary>The configured TOptions instance.</summary>
        public MerakiDashboardClientSettings Value { get; set; }
    }
}
