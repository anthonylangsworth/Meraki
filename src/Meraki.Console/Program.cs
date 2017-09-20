using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Meraki;

namespace Meraki.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed<CommandLineOptions>(clo => Test(clo.ApiKey).Wait());
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine(ex);
            }
        }

        static async Task Test(string apiKey)
        {
            MerakiClient merakiClient = MerakiClientFactory.Create(mcs => mcs.Key = apiKey);

            IReadOnlyList<Organization> organizations = await merakiClient.GetOrganizationsAsync();
            System.Console.WriteLine(string.Join(", ", organizations.Select(org => $"{org.Name} ({org.Id})")));
        }
    }
}
