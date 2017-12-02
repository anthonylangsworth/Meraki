using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Meraki.Test
{
    public class TestMerakiDashboardClientFactory
    {
        [Fact]
        public void Create_ApiKey()
        {
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
            {
                Assert.Equal(MerakiDashboardClientSettingsSetup.DefaultMerakiDashboardApiBaseAddress, merakiDashboardClient.Address.AbsoluteUri);
                Assert.Equal(apiKey, merakiDashboardClient.ApiKey);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ApiKey_Null(string apiKey)
        {
            Assert.Throws<ArgumentException>("apiKey", () => MerakiDashboardClientFactory.Create(apiKey));
        }
    }
}
