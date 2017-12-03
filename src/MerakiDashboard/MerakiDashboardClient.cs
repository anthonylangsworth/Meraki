﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MerakiDashboard
{
    /// <summary>
    /// Wrapper around <see cref="MerakiHttpApiClient"/> for the Meraki Dashboard APIs.
    /// </summary>
    public class MerakiDashboardClient: IDisposable
    {
        /// <summary>
        /// Create a new <see cref="MerakiDashboardClient"/>.
        /// </summary>
        /// <param name="options">
        /// The options to use. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="options"/> cannot be null.
        /// </exception>
        public MerakiDashboardClient(IOptions<MerakiDashboardClientSettings> options)
            : this(options?.Value)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="MerakiDashboardClient"/>.
        /// </summary>
        /// <param name="settings">
        /// The <see cref="MerakiDashboardClientSettings"/> to use. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="settings"/> nor <paramref name="settings.Address"/> can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="MerakiDashboardClientSettings.ApiKey"/> field cannot be null, empty or whitespace.
        /// </exception>
        public MerakiDashboardClient(MerakiDashboardClientSettings settings)
            : this(new MerakiHttpApiClient(settings?.ApiKey, settings?.BaseAddress))
        { 
            // Do nothing
        }

        /// <summary>
        /// Create a <see cref="MerakiDashboardClient"/> with the specified <see cref="MerakiHttpApiClient"/>.
        /// Used for internal testing and mocking only.
        /// </summary>
        /// <param name="apiClient">
        /// The <see cref="MerakiHttpApiClient"/> to use. This cannot be null.
        /// </param>
        /// <param name="urlFormatProvider">
        /// A optional <see cref="UrlFormatProvider"/> used to escape URL arguments.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="apiClient"/> cannot be null.
        /// </exception>
        internal MerakiDashboardClient(MerakiHttpApiClient apiClient, UrlFormatProvider urlFormatProvider = null)
        {
            Client = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            Formatter = urlFormatProvider ?? new UrlFormatProvider();
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~MerakiDashboardClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Implement the dispose patter.
        /// </summary>
        /// <param name="disposing">
        /// true if falled from <see cref="Dispose(bool)"/>, false if called from the
        /// finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Client?.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The <see cref="MerakiHttpApiClient"/> used to call Meraki APIs.
        /// </summary>
        internal MerakiHttpApiClient Client { get; }

        /// <summary>
        /// Used for escaping URL arguments.
        /// </summary>
        internal UrlFormatProvider Formatter { get; }

        /// <summary>
        /// Escape arguments substituted into the string.
        /// </summary>
        /// <param name="formattable">
        /// The string to format, usually in the form of C# 7.0 $"...".
        /// </param>
        /// <returns>
        /// The string with the substituted, escaped values.
        /// </returns>
        internal string InterpolateAndEscape(FormattableString formattable) => formattable.ToString(Formatter);

        // Ignore XML documentation warnings from here on. 
        #pragma warning disable CS1591

        /// <summary>
        /// /devices/[serial]/clients
        /// </summary>
        public virtual async Task<Client[]> GetClientsAsync(string serial)
        {
            return await Client.GetAsync<Client[]>(InterpolateAndEscape($"v0/devices/{serial}/switchPorts"));
        }

        public virtual async Task<string> GetDeviceClientsAsync(string serial, TimeSpan timespan)
        {
            return await Client.GetAsync(InterpolateAndEscape($"v0/devices/{serial}/clients?timespan={(int)timespan.TotalSeconds}"));
        }

        public virtual async Task<string> GetDeviceClientsAsync(string serial)
        {
            return await GetDeviceClientsAsync(serial, TimeSpan.FromSeconds(8600));
        }

        public virtual async Task<Device> GetDeviceAsync(string networkId, string serial)
        {
            return await Client.GetAsync<Device>(InterpolateAndEscape($"v0/networks/{networkId}/devices/{serial}"));
        }

        public virtual async Task<string> GetNetworkAsync(string id)
        {
            return await Client.GetAsync(InterpolateAndEscape($"v0/networks/{id}/admins"));
        }

        public virtual async Task<string> GetNetworkTrafficAsync(string id)
        {
            return await GetNetworkTrafficAsync(id, TimeSpan.FromSeconds(7200));
        }

        public virtual async Task<string> GetNetworkTrafficAsync(string id, TimeSpan timespan)
        {
            return await Client.GetAsync(InterpolateAndEscape($"v0/networks/{id}/traffic?timespan={(int)timespan.TotalSeconds}"));
        }

        public virtual async Task<Device[]> GetNetworkDevicesAsync(string id)
        {
            return await Client.GetAsync<Device[]>(InterpolateAndEscape($"v0/networks/{id}/devices"));
        }

        public virtual async Task<Device[]> GetNetworkDevicesAsync(Network network)
        {
            return await GetNetworkDevicesAsync(network.Id);
        }

        public virtual async Task<string> GetNetworkVlans(string id)
        {
            return await Client.GetAsync(InterpolateAndEscape($"v0/networks/{id}/vlans"));
        }

        public virtual async Task<string> GetNetworkVlans(Network network)
        {
            return await GetNetworkVlans(network.Id);
        }

        public virtual async Task<Organization[]> GetOrganizationsAsync()
        {
            return await Client.GetAsync<Organization[]>($"v0/organizations");
        }

        public virtual async Task<Organization> GetOrganizationAsync(string id)
        {
            return await Client.GetAsync<Organization>(InterpolateAndEscape($"v0/organizations/{id}"));
        }

        public virtual async Task<string> GetOrganizationAdminsAsync(string id)
        {
            return await Client.GetAsync(InterpolateAndEscape($"v0/organizations/{id}/admins"));
        }

        public virtual async Task<string> GetOrganizationAdminsAsync(Organization organization)
        {
            return await GetOrganizationAdminsAsync(organization.Id);
        }

        public virtual async Task<Network[]> GetOrganizationNetworksAsync(string id)
        {
            return await Client.GetAsync<Network[]>(InterpolateAndEscape($"v0/organizations/{id}/networks"));
        }

        public virtual async Task<Network[]> GetOrganizationNetworksAsync(Organization organization)
        {
            return await GetOrganizationNetworksAsync(organization.Id);
        }

        public virtual async Task<Inventory> GetOrganizationInventoryAsync(string id)
        {
            return await Client.GetAsync< Inventory>(InterpolateAndEscape($"v0/organizations/{id}/inventory"));
        }

        public virtual async Task<Inventory> GetOrganizationInventoryAsync(Organization organization)
        {
            return await GetOrganizationInventoryAsync(organization.Id);
        }

        public virtual async Task<LicenseState> GetOrganizationLicenseStateAsync(string id)
        {
            return await Client.GetAsync<LicenseState>(InterpolateAndEscape($"v0/organizations/{id}/licenseState"));
        }

        public virtual async Task<SnmpSettings> GetOrganizationSnmpSettingsAsync(string id)
        {
            return await Client.GetAsync<SnmpSettings>(InterpolateAndEscape($"v0/organizations/{id}/snmp"));
        }

        /// <summary>
        /// /devices/[serial]/switchPorts
        /// </summary>
        public virtual async Task<SwitchPort[]> GetSwitchPortsAsync(string serial)
        {
            return await Client.GetAsync<SwitchPort[]>(InterpolateAndEscape($"v0/devices/{serial}/switchPorts"));
        }
    }
}