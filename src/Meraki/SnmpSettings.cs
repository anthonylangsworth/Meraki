using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki
{
    public class SnmpSettings
    {
        [DataMember(Name= "v2cEnabled")]
        public bool V2cEnabled { get; set; }

        [DataMember(Name= "v3Enabled")]
        public bool V3Enabled { get; set; }

        [DataMember(Name= "v3AuthMode")]
        public string V3AuthMode { get; set; }

        [DataMember(Name= "v3PrivMode")]
        public string V3PrivMode { get; set; }

        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "port")]
        public uint Port { get; set; }
    }
}
