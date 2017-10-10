using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace Meraki.Console
{
    internal class BaseOptions
    {
        [Option('k', "apiKey", HelpText = "Meraki API Key", MetaValue = "API_KEY", Required = true)]
        public string ApiKey { get; set; }
    }
}
