using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meraki.Console
{
    internal class Dump
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            MerakiClient merakiClient = MerakiClientFactory.Create(mcs => mcs.Key = apiKey);

            foreach (Organization organization in await merakiClient.GetOrganizationsAsync())
            {
                System.Console.WriteLine($"{organization.Name} ({organization.Id}):");
                foreach (Network network in await merakiClient.GetOrganizationNetworksAsync(organization))
                {
                    System.Console.WriteLine($"  {network.Name} ({network.Id}):");
                    foreach (Device device in await merakiClient.GetNetworkDevicesAsync(network))
                    {
                        System.Console.WriteLine($"    {device.Serial} ({device.Model}, {device.Mac})");
                    }
                }
            }
        }
    }
}
