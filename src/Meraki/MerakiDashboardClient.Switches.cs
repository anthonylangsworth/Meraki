using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiDashboardClient
    {
        // /devices/[serial]/switchPorts
        public async Task<IReadOnlyList<SwitchPort>> GetSwitchPortsAsync(string serial)
        {
            return await GetAsync<IReadOnlyList<SwitchPort>>(Url($"api/v0/devices/{serial}/switchPorts"));
        }
    }
}
