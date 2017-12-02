using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace MerakiDashboard
{
    public class Inventory
    {
        [DataMember(Name="mac")]
        public string MAC { get; set; }

        [DataMember(Name = "serial")]
        public string Serial { get; set;  }

        [DataMember(Name = "networkId")]
        public string NetworkId { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "claimedAt")]
        public DateTime ClaimedAt { get; set; }

        [DataMember(Name = "publicIp")]
        public IPAddress PublicIpAddress { get; set; }
    }
}
