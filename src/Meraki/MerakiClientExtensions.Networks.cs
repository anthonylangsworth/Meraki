//using System;
//using System.Threading.Tasks;

//namespace Meraki {
//    public static partial class MerakiClientExtensions {
//        public static async Task<string> GetNetworkAsync(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"api/v0/networks/{id}/admins"));
//        }

//        public static async Task<string> GetNetworkTrafficAsync(this MerakiClient client, string id) {
//            return await GetNetworkTrafficAsync(client, id, TimeSpan.FromSeconds(7200));
//        }

//        public static async Task<string> GetNetworkTrafficAsync(this MerakiClient client, string id, TimeSpan timespan) {
//            return await client.GetAsync(Url($"api/v0/networks/{id}/traffic?timespan={(int)timespan.TotalSeconds}"));
//        }

//        public static async Task<string> GetNetworkDevicesAsync(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"api/v0/networks/{id}/devices"));
//        }

//        public static async Task<string> GetNetworkDevicesAsync(this MerakiClient client, Network network) {
//            return await GetNetworkDevicesAsync(client, network.Id);
//        }

//        public static async Task<string> GetNetworkVlans(this MerakiClient client, string id) {
//            return await client.GetAsync(Url($"networks/{id}/vlans"));
//        }

//        public static async Task<string> GetNetworkVlans(this MerakiClient client, Network network) {
//            return await GetNetworkVlans(client, network.Id);
//        }
//    }
//}