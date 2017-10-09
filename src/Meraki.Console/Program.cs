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
                    .WithParsed<CommandLineOptions>(clo => new CiscoLearningLab().Run(clo.ApiKey).Wait());
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine(ex);
            }
        }
    }
}
