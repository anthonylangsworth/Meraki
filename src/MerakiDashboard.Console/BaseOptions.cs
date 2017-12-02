using CommandLine;

namespace MerakiDashboard.Console
{
    internal class BaseOptions
    {
        [Option('k', "apiKey", HelpText = "Meraki API Key", MetaValue = "API_KEY", Required = true)]
        public string ApiKey { get; set; }
    }
}
