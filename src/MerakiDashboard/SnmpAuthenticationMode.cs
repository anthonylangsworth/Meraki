using System;
using System.Collections.Generic;
using System.Text;

namespace MerakiDashboard
{
    /// <summary>
    /// The SNMP authentication mode.
    /// </summary>
    public enum SnmpAuthenticationMode
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Secure Hash Algorithm. Recommended.
        /// </summary>
        Sha,

        /// <summary>
        /// Message Digest 5. Obsolete.
        /// </summary>
        Md5
    }
}
