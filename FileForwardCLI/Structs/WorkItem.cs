using FileForwardCLI.Enumerables;
using FileForwardCLI.Parsers;

namespace FileForwardCLI.Structs
{
    public struct WorkItem
    {
        public CmdLineArgType Type { get; set; }
        public object Item { get; set; }
    }
}