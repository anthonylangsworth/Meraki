using System;
using Microsoft.Extensions.Options;
using Xunit;

namespace MerakiDashboard.Test
{
    /// <summary>
    /// Tests for <see cref="MerakiDashboardClient"/>.
    /// </summary>
    public class TestMerakiDashboardClient
    {
        [Fact]
        public void Ctor_MerakiDashboardClientSettings()
        {
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = baseAddress,
                ApiKey = apiKey
            };

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettings))
            { 
                Assert.Equal(baseAddress, merakiDashboardClient.Client.BaseAddress);
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
            }
        }

        [Fact]
        public void Ctor_IOptions()
        {
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            MerakiDashboardClientSettingsOptions merakiDashboardClientSettingsOptions = new MerakiDashboardClientSettingsOptions
            {
                Value = new MerakiDashboardClientSettings
                {
                    BaseAddress = baseAddress,
                    ApiKey = apiKey
                }
            };

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettingsOptions))
            {
                Assert.Equal(baseAddress, merakiDashboardClient.Client.BaseAddress);
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
            }
        }

        [Fact]
        public void Ctor_Null_MerakiDashboardClientSettings()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient((MerakiDashboardClientSettings) null));
        }

        [Fact]
        public void Ctor_MerakiDashboardClientSettings_Null_BaseAddress()
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = null,
                ApiKey = "apiKey"
            };

            Assert.Throws<ArgumentNullException>("baseAddress", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Ctor_MerakiDashboardClientSettings_Null_ApiKey(string apiKey)
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = new Uri("http://www.myserver.com"),
                ApiKey = apiKey
            };

            Assert.Throws<ArgumentException>("apiKey", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Fact]
        public void Ctor_Null_IOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient((IOptions<MerakiDashboardClientSettings>)null));
        }

        [Fact]
        public void Ctor_IOptions_Null_Settings()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient(new MerakiDashboardClientSettingsOptions()));
        }
    }
}
