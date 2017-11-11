using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiDashboardClient
    {
        // /devices/[serial]/clients
        public async Task<IReadOnlyList<Client>> GetClientsAsync(string serial)
        {
            return await GetAsync<IReadOnlyList<Client>>(Url($"api/v0/devices/{serial}/switchPorts"));
        }
    }
}
