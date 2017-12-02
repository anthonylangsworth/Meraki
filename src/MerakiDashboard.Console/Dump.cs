using System;
using System.Threading.Tasks;

namespace MerakiDashboard.Console
{
    internal class Dump
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(mcs => mcs.Key = apiKey);

            foreach (Organization organization in await merakiDashboardClient.GetOrganizationsAsync())
            {
                System.Console.WriteLine($"{organization.Name} ({organization.Id}):");
                foreach (Network network in await merakiDashboardClient.GetOrganizationNetworksAsync(organization))
                {
                    System.Console.WriteLine($"  {network.Name} ({network.Id}):");
                    foreach (Device device in await merakiDashboardClient.GetNetworkDevicesAsync(network))
                    {
                        System.Console.WriteLine($"    {device.Serial} ({device.Model}, {device.Mac})");
                    }
                }
            }
        }
    }
}
