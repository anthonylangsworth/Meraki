using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
