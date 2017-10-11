using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
