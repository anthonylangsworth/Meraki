using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meraki.Console
{
    /// <summary>
    /// Follow the exercises at https://learninglabs.cisco.com/modules/getting-started-with-meraki/meraki-dashboard-api.
    /// Use the API key "db29550a72d7b61f93057a2a51bfbaf88d80e864".
    /// </summary>
    internal class MerakiDashboardApiLab
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            MerakiClient merakiClient = MerakiClientFactory.Create(mcs => mcs.Key = apiKey);

            const string organizationName = "Meraki Live Sandbox";
            int organizationId = MerakiDashboardHelper.GetOrganizationId(merakiClient, organizationName).Result;

            foreach (Func<MerakiClient, int, Task> exercise in
                new Func<MerakiClient, int, Task>[] { })
            {
                await exercise(merakiClient, organizationId);
            }
        }
    }
}
