using System;
using Microsoft.Extensions.Options;

namespace Meraki {
    public static class MerakiClientFactory {
        public static MerakiClient Create(Action<MerakiClientSettings> configure) {
            var settings = new MerakiClientSettings();
            var options = Options.Create(settings);
            var setup = new MerakiClientSettingsSetup();

            setup.Configure(settings);
            configure?.Invoke(settings);

            return new MerakiClient(options);
        }
    }
}