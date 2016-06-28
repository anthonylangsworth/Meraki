//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Meraki {
//    public static partial class MerakiClientExtensions {
//        public static async Task<IReadOnlyList<Organization>> GetOrganizationsAsync(this MerakiClient client) {
//            return await client.GetAsync<IReadOnlyList<Organization>>($"api/v0/organizations");
//        }

//        public static async Task<string> GetOrganizationAdminsAsync(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"api/v0/organizations/{id}/admins"));
//        }

//        public static async Task<string> GetOrganizationAdminsAsync(this MerakiClient client, Organization organization) {
//            return await client.GetOrganizationAdminsAsync(organization.Id);
//            ;
//        }

//        public static async Task<string> GetOrganizationNetworksAsync(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"api/v0/organizations/{id}/networks"));
//        }

//        public static async Task<string> GetOrganizationNetworksAsync(this MerakiClient client, Organization organization) {
//            return await client.GetOrganizationNetworksAsync(organization.Id);
//        }

//        public static async Task<string> GetOrganizationInventoryAsync(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"api/v0/organizations/{id}/inventory"));
//        }

//        public static async Task<string> GetOrganizationInventoryAsync(this MerakiClient client, Organization organization) {
//            return await GetOrganizationInventoryAsync(client, organization.Id);
//        }
//    }
//}