using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace MerakiDashboard
{
    [DataContract]
    public class SnmpPutSettings
    {
        [DataMember(Name= "v2cEnabled")]
        public bool V2cEnabled { get; set; }

        [DataMember(Name= "v3Enabled")]
        public bool V3Enabled { get; set; }

        [DataMember(Name= "v3AuthMode")]
        public string V3AuthenticationMode { get; set; }

        [DataMember(Name = "v3AuthPass")]
        public string V3AuthenticationPassword { get; set; }

        [DataMember(Name= "v3PrivMode")]
        public string V3PrivacyMode { get; set; }

        [DataMember(Name = "v3PrivPass")]
        public string V3PrivacyPassword { get; set; }

        [DataMember(Name = "peerIps")]
        public string PeerIpsRaw
        {
            get => string.Join(';', PeerIps);
            set => PeerIps = value.Split(";").Select(IPAddress.Parse);
        }

        [IgnoreDataMember]
        public IEnumerable<IPAddress> PeerIps { get; set; }
    }
}
