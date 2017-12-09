using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace MerakiDashboard.Test
{
    public class TestDevice
    {
        [Theory]
        [MemberData(nameof(PublicIpAddressRawTestData))]
        public void LanIpAddressRaw(string raw, IPAddress expectedIpAddress)
        {
            Device inventoryEntry = new Device { LanIpAddressRaw = raw };
            Assert.Equal(expectedIpAddress, inventoryEntry.LanIpAddress);
            Assert.Equal(raw, inventoryEntry.LanIpAddressRaw);
        }

        public static IEnumerable<object[]> PublicIpAddressRawTestData()
        {
            return TestInventoryEntry.PublicIpAddressRawTestData();
        }
    }
}
