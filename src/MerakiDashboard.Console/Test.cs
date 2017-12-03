using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MerakiDashboard.Console
{
    internal class Test
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            using (MerakiDashboardClient merakiDashboardClient =
                MerakiDashboardClientFactory.Create(mcs => mcs.ApiKey = apiKey))
            using(new MerakiHttpApiDebugContext())
            {
                string organizationId = merakiDashboardClient.GetOrganizationsAsync().Result.First().Id;
                await GetSnmpSettings(organizationId, merakiDashboardClient);
                // await PutSnmpSettings(merakiDashboardClient);
            }
        }

        private static async Task GetSnmpSettings(string organizationId, MerakiDashboardClient merakiDashboardClient)
        {
            SnmpGetSettings snmpGetSettings = await merakiDashboardClient.GetOrganizationSnmpSettingsAsync(organizationId);
            System.Console.Out.WriteLine(JsonConvert.SerializeObject(snmpGetSettings));
        }

        private static async Task PutSnmpSettings(string organizationId, MerakiDashboardClient merakiDashboardClient)
        {
            SnmpPutSettings snmpPutSettings = new SnmpPutSettings
            {
                V2cEnabled = false,
                V3Enabled = true,
                V3AuthenticationMode = ""
            };

            await merakiDashboardClient.PutOrganizationSnmpSettingsAsync(organizationId, snmpPutSettings);
        }
    }
}
