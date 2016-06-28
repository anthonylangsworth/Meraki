//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

//namespace Meraki {
//    public static partial class MerakiClientExtensions {
//        private static readonly UrlFormatProvider _formatter = new UrlFormatProvider();

//        private static string Url(FormattableString formattable) => formattable.ToString(_formatter);

//        internal static async Task<T> GetAsync<T>(this MerakiClient client, string uri) {
//            if(client == null) {
//                throw new ArgumentNullException(nameof(client));
//            }

//            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
//            var content = await response.Content.ReadAsStringAsync();

//            return JsonConvert.DeserializeObject<T>(content);
//        }

//        public static async Task<string> GetAsync(this MerakiClient client, string uri) {
//            if(client == null) {
//                throw new ArgumentNullException(nameof(client));
//            }

//            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
//            var content = await response.Content.ReadAsStringAsync();

//            return content;
//        }

    

        

        
//    }
//}