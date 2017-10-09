using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiClient
    {
        public async Task<string> GetNetworkAsync(string id)
        {
            return await GetAsync(Url($"api/v0/networks/{id}/admins"));
        }

        public async Task<string> GetNetworkTrafficAsync(string id)
        {
            return await GetNetworkTrafficAsync(id, TimeSpan.FromSeconds(7200));
        }

        public async Task<string> GetNetworkTrafficAsync(string id, TimeSpan timespan)
        {
            return await GetAsync(Url($"api/v0/networks/{id}/traffic?timespan={(int)timespan.TotalSeconds}"));
        }

        public async Task<IReadOnlyList<Device>> GetNetworkDevicesAsync(string id)
        {
            return await GetAsync<IReadOnlyList<Device>>(Url($"api/v0/networks/{id}/devices"));
        }

        public async Task<IReadOnlyList<Device>> GetNetworkDevicesAsync(Network network)
        {
            return await GetNetworkDevicesAsync(network.Id);
        }

        public async Task<string> GetNetworkVlans(string id)
        {
            return await GetAsync(Url($"networks/{id}/vlans"));
        }

        public async Task<string> GetNetworkVlans(Network network)
        {
            return await GetNetworkVlans(network.Id);
        }
    }
}