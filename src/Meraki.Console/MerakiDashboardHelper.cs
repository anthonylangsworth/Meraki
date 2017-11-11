using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meraki.Console
{
    internal static class MerakiDashboardHelper
    {
        public static async Task<int> GetOrganizationId(MerakiDashboardClient merakiDashboardClient, string organizationName)
        {
            IReadOnlyList<Organization> organizations = await merakiDashboardClient.GetOrganizationsAsync();
            return organizations.First(o => o.Name == organizationName).Id;
        }
    }
}
