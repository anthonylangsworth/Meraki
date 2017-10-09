using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meraki.Console
{
    /// <summary>
    /// Follow the exercises at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab.
    /// </summary>
    public class CiscoLearningLab
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            MerakiClient merakiClient = MerakiClientFactory.Create(mcs => mcs.Key = apiKey);

            const string organizationName = "Meraki Live Sandbox";
            int organizationId = GetOrganizationId(merakiClient, organizationName).Result;

            foreach (Func<MerakiClient, int, Task> exercise in
                new Func<MerakiClient, int, Task>[] { Exercise1, Exercise2, Exercise3 })
            {
                await exercise(merakiClient, organizationId);
            }
        }

        private async Task<int> GetOrganizationId(MerakiClient merakiClient, string organizationName)
        {
            IReadOnlyList<Organization> organizations = await merakiClient.GetOrganizationsAsync();
            return organizations.First(o => o.Name == organizationName).Id;
        }

        /// <summary>
        /// Determine the Organization ID for the “Meraki Live Sandbox”
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <returns></returns>
        private async Task Exercise1(MerakiClient merakiClient, int organizationId)
        {
            await System.Console.Out.WriteLineAsync($"ID {organizationId}");
        }

        /// <summary>
        /// Find out through the API when the license of this organization expires.
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <returns></returns>
        private async Task Exercise2(MerakiClient merakiClient, int organizationId)
        {
            LicenseState licenseState = await merakiClient.GetOrganizationLicenseStateAsync(organizationId);
            await System.Console.Out.WriteLineAsync($"The license expires on {licenseState.ExpirationDate}");
        }

        /// <summary>
        /// Is SNMP enabled on this Organization?
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <returns></returns>
        private async Task Exercise3(MerakiClient merakiClient, int organizationId)
        {
            SnmpSettings snmpSettings = await merakiClient.GetOrganizationSnmpSettingsAsync(organizationId);
            await System.Console.Out.WriteLineAsync($"SNMP v2c enabled: {snmpSettings.V2cEnabled},  v3 enabled: {snmpSettings.V3Enabled}");
        }
    }
}
