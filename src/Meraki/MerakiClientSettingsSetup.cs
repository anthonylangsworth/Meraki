using Microsoft.Extensions.Options;

namespace Meraki {
    public class MerakiClientSettingsSetup : ConfigureOptions<MerakiClientSettings> {
        public MerakiClientSettingsSetup() : base(ConfigureOptions) {
        }

        private static void ConfigureOptions(MerakiClientSettings options) {
            options.Address = "https://dashboard.meraki.com";
            options.Key = "";
        }
    }
}