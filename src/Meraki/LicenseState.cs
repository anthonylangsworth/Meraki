using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

// Ignore XML documentation warnings from here on. 
#pragma warning disable CS1591

namespace Meraki
{
    public class LicenseState
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        // TODO: Convert to DateTime
        [DataMember(Name = "expiryDate")]
        public string ExpirationDate { get; set; }

        [DataMember(Name = "licensedDeviceCounts")]
        public IDictionary<string, uint> LicensedDeviceCounts { get; set; }
    }
}
