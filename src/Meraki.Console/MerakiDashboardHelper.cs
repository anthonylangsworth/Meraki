using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meraki.Console
{
    internal static class MerakiDashboardHelper
    {
        public static async Task<int> GetOrganizationId(MerakiClient merakiClient, string organizationName)
        {
            IReadOnlyList<Organization> organizations = await merakiClient.GetOrganizationsAsync();
            return organizations.First(o => o.Name == organizationName).Id;
        }
    }
}
