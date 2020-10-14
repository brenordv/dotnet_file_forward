using System;
using System.Net;

namespace FileForwardCLI.Utils
{
    public class Printer
    {
        private int _itemCount;

        public void PrintSuccess(HttpStatusCode statusCode)
        {
            _itemCount++;
            Console.WriteLine($"[{statusCode}] Item {_itemCount:D3} finished successfully!");
        }

        public void PrintFail(HttpStatusCode statusCode, string reasonPhrase)
        {
            _itemCount++;
            Console.WriteLine($"[{statusCode}] Item {_itemCount:D3} failed! {reasonPhrase}");
        }
    }
}