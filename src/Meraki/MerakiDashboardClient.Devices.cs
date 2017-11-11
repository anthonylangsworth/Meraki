using System;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiDashboardClient
    {
        public async Task<string> GetDeviceClientsAsync(string serial, TimeSpan timespan)
        {
            return await GetAsync(Url($"api/v0/devices/{serial}/clients?timespan={(int)timespan.TotalSeconds}"));
        }

        public async Task<string> GetDeviceClientsAsync(string serial)
        {
            return await GetDeviceClientsAsync(serial, TimeSpan.FromSeconds(8600));
        }

        public async Task<Device> GetDeviceAsync(string networkId, string serial)
        {
            return await GetAsync<Device>(Url($"api/v0/networks/{networkId}/devices/{serial}"));
        }
    }
}