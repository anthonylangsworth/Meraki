using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Meraki
{
    /// <summary>
    /// A Meraki Network.
    /// </summary>
    public class Network
    {
        [DataMember(Name="id")]
        public int Id { get; set; }
    }
}
