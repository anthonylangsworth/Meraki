using System;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiClient
    {
        public async Task<string> GetNetworkAsync(int id)
        {
            return await GetAsync(Url($"api/v0/networks/{id}/admins"));
        }

        public async Task<string> GetNetworkTrafficAsync(int id)
        {
            return await GetNetworkTrafficAsync(id, TimeSpan.FromSeconds(7200));
        }

        public async Task<string> GetNetworkTrafficAsync(int id, TimeSpan timespan)
        {
            return await GetAsync(Url($"api/v0/networks/{id}/traffic?timespan={(int)timespan.TotalSeconds}"));
        }

        public async Task<string> GetNetworkDevicesAsync(int id)
        {
            return await GetAsync(Url($"api/v0/networks/{id}/devices"));
        }

        public async Task<string> GetNetworkDevicesAsync(Network network)
        {
            return await GetNetworkDevicesAsync(network.Id);
        }

        public async Task<string> GetNetworkVlans(int id)
        {
            return await GetAsync(Url($"networks/{id}/vlans"));
        }

        public async Task<string> GetNetworkVlans(Network network)
        {
            return await GetNetworkVlans(network.Id);
        }
    }
}