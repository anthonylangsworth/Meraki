using System.Linq;
using System.Threading.Tasks;

namespace MerakiDashboard.Console
{
    internal static class MerakiDashboardHelper
    {
        public static async Task<string> GetOrganizationId(MerakiDashboardClient merakiDashboardClient, string organizationName)
        {
            Organization[] organizations = await merakiDashboardClient.GetOrganizationsAsync();
            return organizations.First(o => o.Name == organizationName).Id;
        }
    }
}
