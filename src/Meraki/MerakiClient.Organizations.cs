using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meraki
{
    public partial class MerakiClient
    {
        public async Task<IReadOnlyList<Organization>> GetOrganizationsAsync()
        {
            return await GetAsync<IReadOnlyList<Organization>>($"api/v0/organizations");
        }

        public async Task<string> GetOrganizationAdminsAsync(int id)
        {
            return await GetAsync(Url($"api/v0/organizations/{id}/admins"));
        }

        public async Task<string> GetOrganizationAdminsAsync(Organization organization)
        {
            return await GetOrganizationAdminsAsync(organization.Id);
        }

        public async Task<string> GetOrganizationNetworksAsync(int id)
        {
            return await GetAsync(Url($"api/v0/organizations/{id}/networks"));
        }

        public async Task<string> GetOrganizationNetworksAsync(Organization organization)
        {
            return await GetOrganizationNetworksAsync(organization.Id);
        }

        public async Task<string> GetOrganizationInventoryAsync(int id)
        {
            return await GetAsync(Url($"api/v0/organizations/{id}/inventory"));
        }

        public async Task<string> GetOrganizationInventoryAsync(Organization organization)
        {
            return await GetOrganizationInventoryAsync(organization.Id);
        }

        public async Task<LicenseState> GetOrganizationLicenseStateAsync(int id)
        {
            return await GetAsync<LicenseState>(Url($"api/v0/organizations/{id}/licenseState"));
        }

        public async Task<SnmpSettings> GetOrganizationSnmpSettingsAsync(int id)
        {
            return await GetAsync<SnmpSettings>(Url($"api/v0/organizations/{id}/snmp"));
        }
    }
}