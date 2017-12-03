using CommandLine;

namespace MerakiDashboard.Console
{
    [Verb("test", HelpText = "Run arbitrary tests. Used for whatever tests are needed on the MerakiDashboard assembly.")]
    internal class TestOptions : BaseOptions
    {
        // No members
    }
}
