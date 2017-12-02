using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meraki
{
    /// <summary>
    /// Wrapper around Meraki APIs.
    /// </summary>
    public sealed class MerakiDashboardClient: IDisposable
    {
        private const string AcceptTypeHttpHeader = "Accept-Type";
        private const string MerakiApiKeyHttpHeader = "X-Cisco-Meraki-API-Key";

        private readonly HttpClient _client;
        private readonly UrlFormatProvider _formatter = new UrlFormatProvider();

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
        /// The <see cref="MerakiDashboardClientSettings.Key"/> field cannot be null, empty or whitespace.
        /// </exception>
        public MerakiDashboardClient(MerakiDashboardClientSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (settings.Address == null)
            {
                throw new ArgumentException("Missing address", nameof(settings));
            }
            if (string.IsNullOrWhiteSpace(settings.Key))
            {
                throw new ArgumentException("Missing API key", nameof(settings));
            }

            _client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(settings.Address.AbsoluteUri)
            };
            _client.DefaultRequestHeaders.Add(MerakiApiKeyHttpHeader, settings.Key);
            _client.DefaultRequestHeaders.Add(AcceptTypeHttpHeader, "application/json");
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Will expand this into the full Dispose pattern if or when this class is inheritable.
            _client?.Dispose();
        }

        /// <summary>
        /// Base address for Meraki API calls.
        /// </summary>
        public Uri Address => _client.BaseAddress;

        /// <summary>
        /// The Meraki API key used.
        /// </summary>
        public string ApiKey => _client.DefaultRequestHeaders.GetValues(MerakiApiKeyHttpHeader).FirstOrDefault();

        private string Url(FormattableString formattable) => formattable.ToString(_formatter);

        /// <summary>
        /// Call the given URL asynchronously, not expecting any returned data.
        /// </summary>
        /// <param name="method">
        /// </param>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri)
        {
            using (HttpResponseMessage response = await SendAsync(new HttpRequestMessage(method, uri)))
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
        } 

        internal async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (HttpResponseMessage response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        internal async Task<T> GetAsync<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(uri));
        }

        /// <summary>
        /// Call the given URL asynchronously, expecting returned data.
        /// </summary>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The result as a string.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public async Task<string> GetAsync(string uri)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
            using (HttpResponseMessage response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        // /devices/[serial]/clients
        public async Task<IReadOnlyList<Client>> GetClientsAsync(string serial)
        {
            return await GetAsync<IReadOnlyList<Client>>(Url($"api/v0/devices/{serial}/switchPorts"));
        }

        public async Task<string> GetDeviceClientsAsync(string serial, TimeSpan timespan)
        {
            return await GetAsync(Url($"api/v0/devices/{serial}/clients?timespan={(int)timespan.TotalSeconds}"));
        }

        public async Task<string> GetDeviceClientsAsync(string serial)
        {
            return await GetDeviceClientsAsync(serial, TimeSpan.FromSeconds(8600));
        }

        public async Task<Device> GetDeviceAsync(string networkId, string serial)
        {
            return await GetAsync<Device>(Url($"api/v0/networks/{networkId}/devices/{serial}"));
        }

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

        public async Task<IReadOnlyList<Organization>> GetOrganizationsAsync()
        {
            return await GetAsync<IReadOnlyList<Organization>>($"api/v0/organizations");
        }

        public async Task<string> GetOrganizationAdminsAsync(string id)
        {
            return await GetAsync(Url($"api/v0/organizations/{id}/admins"));
        }

        public async Task<string> GetOrganizationAdminsAsync(Organization organization)
        {
            return await GetOrganizationAdminsAsync(organization.Id);
        }

        public async Task<IReadOnlyList<Network>> GetOrganizationNetworksAsync(string id)
        {
            return await GetAsync<IReadOnlyList<Network>>(Url($"api/v0/organizations/{id}/networks"));
        }

        public async Task<IReadOnlyList<Network>> GetOrganizationNetworksAsync(Organization organization)
        {
            return await GetOrganizationNetworksAsync(organization.Id);
        }

        public async Task<string> GetOrganizationInventoryAsync(string id)
        {
            return await GetAsync(Url($"api/v0/organizations/{id}/inventory"));
        }

        public async Task<string> GetOrganizationInventoryAsync(Organization organization)
        {
            return await GetOrganizationInventoryAsync(organization.Id);
        }

        public async Task<LicenseState> GetOrganizationLicenseStateAsync(string id)
        {
            return await GetAsync<LicenseState>(Url($"api/v0/organizations/{id}/licenseState"));
        }

        public async Task<SnmpSettings> GetOrganizationSnmpSettingsAsync(string id)
        {
            return await GetAsync<SnmpSettings>(Url($"api/v0/organizations/{id}/snmp"));
        }

        // /devices/[serial]/switchPorts
        public async Task<IReadOnlyList<SwitchPort>> GetSwitchPortsAsync(string serial)
        {
            return await GetAsync<IReadOnlyList<SwitchPort>>(Url($"api/v0/devices/{serial}/switchPorts"));
        }
    }
}