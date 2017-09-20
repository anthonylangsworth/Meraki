using System;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiClient
    {
        public async Task<string> GetDeviceClientsAsync(string serial, TimeSpan timespan)
        {
            return await GetAsync(Url($"api/v0/devices/{serial}/clients?timespan={(int)timespan.TotalSeconds}"));
        }

        public async Task<string> GetDeviceClientsAsync(string serial)
        {
            return await GetDeviceClientsAsync(serial, TimeSpan.FromSeconds(8600));
        }
    }
}