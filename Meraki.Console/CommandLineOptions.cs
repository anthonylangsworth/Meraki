using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace Meraki.Console
{
    class CommandLineOptions
    {
        [Option('k', "apiKey", HelpText = "Meraki API Key", MetaValue = "API_KEY")]
        public string ApiKey { get; set; }
    }
}
