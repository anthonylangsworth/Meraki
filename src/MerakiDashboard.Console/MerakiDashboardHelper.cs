using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerakiDashboard.Console
{
    internal static class MerakiDashboardHelper
    {
        public static async Task<string> GetOrganizationId(MerakiDashboardClient merakiDashboardClient, string organizationName)
        {
            IReadOnlyList<Organization> organizations = await merakiDashboardClient.GetOrganizationsAsync();
            return organizations.First(o => o.Name == organizationName).Id;
        }
    }
}
