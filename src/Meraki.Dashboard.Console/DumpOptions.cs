using CommandLine;

namespace MerakiDashboard.Console
{
    [Verb("dump", HelpText = "Dump devices by network and inventory from all organizations the API key can access.")]
    internal class DumpOptions: BaseOptions
    {
        // No members
    }
}
