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
                new Func<MerakiClient, int, Task>[] { Exercise6 }) // Exercise1, Exercise2, Exercise3, Exercise4, Exercise5
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
        /// <param name="organizationId"></param>
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
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise3(MerakiClient merakiClient, int organizationId)
        {
            SnmpSettings snmpSettings = await merakiClient.GetOrganizationSnmpSettingsAsync(organizationId);
            await System.Console.Out.WriteLineAsync($"SNMP v2c enabled: {snmpSettings.V2cEnabled},  v3 enabled: {snmpSettings.V3Enabled}");
        }

        /// <summary>
        /// What is the value for “claimedAt” for the device with serial number "Q2JD-W28X-FNEN"
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise4(MerakiClient merakiClient, int organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2JD-W28X-FNEN" does not exist
            Device device = merakiClient.GetOrganizationNetworksAsync(organizationId).Result
                                        .SelectMany(n => merakiClient.GetNetworkDevicesAsync(n.Id).Result)
                                        .FirstOrDefault(d => deviceSerial.Equals(d.Serial, StringComparison.OrdinalIgnoreCase)); 
            await System.Console.Out.WriteLineAsync($"Device {deviceSerial} claimed at {device.Mac}"); // claimedAt does not exist
        }

        /// <summary>
        /// What’s the name of the network that contains the device with serial number “Q2CD-MJ68-SYFF”
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise5(MerakiClient merakiClient, int organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2CD-MJ68-SYFF" does not exist
            Network network = merakiClient.GetOrganizationNetworksAsync(organizationId).Result
                                          .FirstOrDefault(n => merakiClient.GetNetworkDevicesAsync(n.Id).Result.Any(d => deviceSerial.Equals(d.Serial)));
            await System.Console.Out.WriteLineAsync($"Network '{network.Name}' contains device with serial {deviceSerial}"); // claimedAt does not exist
        }

        /// <summary>
        /// What tags are applied to device “Q2HP-AJ22-UG72”?
        /// </summary>
        /// <param name="merakiClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise6(MerakiClient merakiClient, int organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2HP-AJ22-UG72" does not exist
            Device device = merakiClient.GetOrganizationNetworksAsync(organizationId).Result
                                        .SelectMany(n => merakiClient.GetNetworkDevicesAsync(n.Id).Result)
                                        .FirstOrDefault(d => deviceSerial.Equals(d.Serial, StringComparison.OrdinalIgnoreCase)); 
            await System.Console.Out.WriteLineAsync($"Device with serial {deviceSerial} has the tags '{device.Tags}'");
        }
    }
}
