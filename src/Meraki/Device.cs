using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

// Ignore XML documentation warnings from here on. 
#pragma warning disable CS1591

namespace Meraki
{
    public class Device
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="lat")]
        public double Lattitude { get; set; }

        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        [DataMember(Name = "serial")]
        public string Serial { get; set; }

        [DataMember(Name = "mac")]
        public string Mac { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "lanIp")]
        public IPAddress LapIp { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "networkdId")]
        public string NetworkId { get; set; }
    }
}
