using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Meraki.Test
{
    class MerakiDashboardClientSettingsOptions: IOptions<MerakiDashboardClientSettings>
    {
        /// <summary>The configured TOptions instance.</summary>
        public MerakiDashboardClientSettings Value { get; set; }
    }
}
