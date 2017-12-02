using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using Xunit;

namespace Meraki.Test
{
    public class TestMerakiDashboardClient
    {
        [Fact]
        public void Ctor_MerakiDashboardClientSettings()
        {
            MerakiDashboardClient merakiDashboardClient;
            MerakiDashboardClientSettings merakiDashboardClientSettings;
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            merakiDashboardClientSettings = new MerakiDashboardClientSettings();
            merakiDashboardClientSettings.Address = baseAddress;
            merakiDashboardClientSettings.Key = apiKey;

            merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettings);

            Assert.Equal(baseAddress, merakiDashboardClient.Address);
            Assert.Equal(apiKey, merakiDashboardClient.ApiKey);
        }

        [Fact]
        public void Ctor_IOptions()
        {
            MerakiDashboardClientSettingsOptions merakiDashboardClientSettingsOptions;
            MerakiDashboardClient merakiDashboardClient;
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            merakiDashboardClientSettingsOptions = new MerakiDashboardClientSettingsOptions();
            merakiDashboardClientSettingsOptions.Value = new MerakiDashboardClientSettings();
            merakiDashboardClientSettingsOptions.Value.Address = baseAddress;
            merakiDashboardClientSettingsOptions.Value.Key = apiKey;

            merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettingsOptions);

            Assert.Equal(baseAddress, merakiDashboardClient.Address);
            Assert.Equal(apiKey, merakiDashboardClient.ApiKey);
        }

        [Fact]
        public void Ctor_Null_MerakiDashboardClientSettings()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient((MerakiDashboardClientSettings) null));
        }

        [Fact]
        public void Ctor_MerakiDashboardClientSettings_Null_Address()
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings;

            merakiDashboardClientSettings = new MerakiDashboardClientSettings();
            merakiDashboardClientSettings.Address = null;
            merakiDashboardClientSettings.Key = "apiKey";

            Assert.Throws<ArgumentException>("settings", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Ctor_MerakiDashboardClientSettings_Null_Key(string apiKey)
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings;

            merakiDashboardClientSettings = new MerakiDashboardClientSettings();
            merakiDashboardClientSettings.Address = new Uri("http://www.myserver.com");
            merakiDashboardClientSettings.Key = apiKey;

            Assert.Throws<ArgumentException>("settings", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Fact]
        public void Ctor_Null_IOptions()
        {
            Assert.Throws<ArgumentNullException>("settings", () => new MerakiDashboardClient((IOptions<MerakiDashboardClientSettings>)null));
        }

        [Fact]
        public void Ctor_IOptions_Null_Settings()
        {
            Assert.Throws<ArgumentNullException>("settings", () => new MerakiDashboardClient(new MerakiDashboardClientSettingsOptions()));
        }
    }
}
