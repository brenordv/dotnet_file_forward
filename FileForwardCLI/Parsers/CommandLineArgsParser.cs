using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FileForward.Exceptions;
using FileForwardCLI.Converters;
using FileForwardCLI.Enumerables;
using FileForwardCLI.Structs;

namespace FileForwardCLI.Parsers
{
    public static class CommandLineArgsParser
    {
        public static List<WorkItem> GetWorkItems(string[] args)
        {
            if (args.Length == 0)
                throw new OperationException("No Command line arguments passed!");

            var results = new List<WorkItem>();
            for (var i = 0; i < args.Length; i++)
            {
                var byteResult = StringConverter.ToByteArray(args[i]);
                if (byteResult.LongLength > 0)
                {
                    results.Add(new WorkItem
                    {
                        Type = CmdLineArgType.ByteArray,
                        Item = byteResult
                    });
                    continue;
                }

                var argType = GetArgFileType(args[i]);
                if (argType == CmdLineArgType.Invalid)
                    continue;
                
                results.Add(new WorkItem
                {
                    Type = argType,
                    Item = args[i]
                });
            }

            return results;
        }

        private static CmdLineArgType GetArgFileType(string arg)
        {
            try
            {
                var attr = File.GetAttributes(arg);
                return (attr & FileAttributes.Directory) != FileAttributes.Directory
                    ? CmdLineArgType.Folder
                    : CmdLineArgType.File;
            }
            catch (FileNotFoundException)
            {
                return CmdLineArgType.Invalid;
            }
        }
    }
}