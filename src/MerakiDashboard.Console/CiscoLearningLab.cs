using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meraki.Console;

namespace MerakiDashboard.Console
{
    /// <summary>
    /// Follow the exercises at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab.
    /// </summary>
    internal class CiscoLearningLab
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
            {
                const string organizationName = "Meraki Live Demo";
                string organizationId = MerakiDashboardHelper.GetOrganizationId(merakiDashboardClient, organizationName).Result;

                foreach (Func<MerakiDashboardClient, string, Task> exercise in
                    new Func<MerakiDashboardClient, string, Task>[]
                    {
                        Exercise1, Exercise2, Exercise3, Exercise4, Exercise5, Exercise6, Exercise7, Exercise8
                    })
                {
                    await exercise(merakiDashboardClient, organizationId);
                }
            }
        }

        /// <summary>
        /// Determine the Organization ID for the “Meraki Live Sandbox”
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise1(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            await System.Console.Out.WriteLineAsync($"ID {organizationId}");
        }

        /// <summary>
        /// Find out through the API when the license of this organization expires.
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise2(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            LicenseState licenseState = await merakiDashboardClient.GetOrganizationLicenseStateAsync(organizationId);
            await System.Console.Out.WriteLineAsync($"The license expires on {licenseState.ExpirationDate}");
        }

        /// <summary>
        /// Is SNMP enabled on this Organization?
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise3(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            SnmpSettings snmpSettings = await merakiDashboardClient.GetOrganizationSnmpSettingsAsync(organizationId);
            await System.Console.Out.WriteLineAsync($"SNMP v2c enabled: {snmpSettings.V2cEnabled},  v3 enabled: {snmpSettings.V3Enabled}");
        }

        /// <summary>
        /// What is the value for “claimedAt” for the device with serial number "Q2JD-W28X-FNEN"
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise4(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2JD-W28X-FNEN" does not exist
            Device device = merakiDashboardClient.GetOrganizationNetworksAsync(organizationId).Result
                                        .SelectMany(n => merakiDashboardClient.GetNetworkDevicesAsync(n.Id).Result)
                                        .FirstOrDefault(d => deviceSerial.Equals(d.Serial, StringComparison.OrdinalIgnoreCase)); 
            await System.Console.Out.WriteLineAsync($"Device {deviceSerial} claimed at {device.Mac}"); // claimedAt does not exist
        }

        /// <summary>
        /// What’s the name of the network that contains the device with serial number “Q2CD-MJ68-SYFF”
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise5(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2CD-MJ68-SYFF" does not exist
            Network network = merakiDashboardClient.GetOrganizationNetworksAsync(organizationId).Result
                                          .FirstOrDefault(n => merakiDashboardClient.GetNetworkDevicesAsync(n.Id).Result.Any(d => deviceSerial.Equals(d.Serial)));
            await System.Console.Out.WriteLineAsync($"Network '{network?.Name}' contains device with serial {deviceSerial}"); // claimedAt does not exist
        }

        /// <summary>
        /// What tags are applied to device “Q2HP-AJ22-UG72”?
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise6(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            const string deviceSerial = "Q2EK-S3AA-BXFW"; // "Q2HP-AJ22-UG72" does not exist
            Device device = GetDevice(merakiDashboardClient, organizationId, deviceSerial);
            await System.Console.Out.WriteLineAsync($"Device with serial {deviceSerial} has the tags '{device?.Tags}'");
        }

        /// <summary>
        /// To which port of “Q2HP-AJ22-UG72” is the client, “e0:55:3d:1f:a7:10” connected?
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise7(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            const string switchSerial = "Q2HP-DT5F-KMJE"; // "Q2HP-AJ22-UG72" does not exist
            const string deviceMac = "e0:55:3d:4f:45:a9"; // "Q2AT-6CLF-RQFE", an MC7
            Client[] clients = await merakiDashboardClient.GetClientsAsync(switchSerial);
            await System.Console.Out.WriteLineAsync($"Switch {switchSerial} has device '{deviceMac} the on port '{clients.FirstOrDefault(c => deviceMac.Equals(c.Mac))?.SwitchPort}'");
        }


        /// <summary>
        /// How many VLANs are configured on device “Q2QN-WPR6-UJPL”?
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private async Task Exercise8(MerakiDashboardClient merakiDashboardClient, string organizationId)
        {
            const string switchSerial = "Q2HP-DT5F-KMJE"; // "Q2QN-WPR6-UJPL" does not exist
            SwitchPort[] ports = await merakiDashboardClient.GetSwitchPortsAsync(switchSerial);
            await System.Console.Out.WriteLineAsync($"Device with serial {switchSerial} has '{ports.Select(sp => sp.Vlan).Distinct().Count()}' vlans");
        }

        /// <summary>
        /// Get the <see cref="Device"/> for the given serial number.
        /// </summary>
        /// <param name="merakiDashboardClient"></param>
        /// <param name="organizationId"></param>
        /// <param name="serial"></param>
        /// <returns>
        /// The <see cref="Device"/> or null, if no device exists.
        /// </returns>
        private Device GetDevice(MerakiDashboardClient merakiDashboardClient, string organizationId, string serial)
        {
            return merakiDashboardClient.GetOrganizationNetworksAsync(organizationId).Result
                               .SelectMany(n => merakiDashboardClient.GetNetworkDevicesAsync(n.Id).Result)
                               .FirstOrDefault(d => serial.Equals(d.Serial, StringComparison.OrdinalIgnoreCase));
        }
    }
}
