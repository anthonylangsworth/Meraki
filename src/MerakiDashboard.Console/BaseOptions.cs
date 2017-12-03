﻿using System;
using CommandLine;

namespace MerakiDashboard.Console
{
    internal class BaseOptions
    {
        private const string ApiKeyEnvironmentVariable = "MERAKI_API_KEY";

        private string apiKey;

        [Option('k', "apiKey", HelpText = "Meraki API Key", MetaValue = "API_KEY", Required = false)]
        public string ApiKey
        {
            get
            {
                string result;
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    result = apiKey;
                }
                else
                {
                    result = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        throw new ArgumentException($"Meraki API Key not specified on the command line or in environment variable '{ApiKeyEnvironmentVariable}'");
                    }
                }
                return result;
            }
            set => apiKey = value;
        }
    }
}
