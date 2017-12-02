using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki
{
    public class Usage
    {
        [DataMember(Name="sent")]
        public uint Sent { get; set; }

        [DataMember(Name = "received")]
        public uint Received { get; set; }
    }
}
