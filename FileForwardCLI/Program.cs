using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FileForward;
using FileForwardCLI.Enumerables;
using FileForwardCLI.Parsers;
using FileForwardCLI.Structs;
using FileForwardCLI.Utils;
using Microsoft.Extensions.Configuration;

namespace FileForwardCLI
{
    class Program
    {
        private static IConfiguration _config;
        private static ForwardEngine _fileForward;
        private static Printer _printer;
        
        static void Main(string[] args)
        {
            Console.WriteLine("File Forward CLI");
            Console.WriteLine("Initializing...");
            Startup();
            ExecuteFromCommandLine(args);

            Console.WriteLine("All done!");
        }
        
        private static void Startup()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            _fileForward = new ForwardEngine(_config["baseUrl"]);
            _printer = new Printer();
        }
        
        private static void ExecuteFromCommandLine(string[] args)
        {
            CommandLineArgsParser.GetWorkItems(args).ForEach(ExecuteSingleWorkItem);
        }

        private static void ExecuteSingleWorkItem(WorkItem workItem)
        {
            Task<HttpResponseMessage> response;
            switch (workItem.Type)
            {
                case CmdLineArgType.File:
                    response = _fileForward.PostBytes("/photos", workItem.Item.ToString());
                    break;

                case CmdLineArgType.ByteArray:
                    response = _fileForward.PostBytes("/photos", (byte[])workItem.Item);
                    break;
                        
                case CmdLineArgType.Folder:
                    Directory.GetFiles(workItem.Item.ToString()).ToList().ForEach(file =>
                    {
                        ExecuteSingleWorkItem(new WorkItem
                        {
                            Type = CmdLineArgType.File,
                            Item = file.ToString()
                        });
                    });
                    return;
                
                default:
                    return;
            }

            var result = response.Result;
            
            if (result.IsSuccessStatusCode)
                _printer.PrintSuccess(result.StatusCode);
            else
                _printer.PrintFail(result.StatusCode, result.ReasonPhrase);
        }
    }
}