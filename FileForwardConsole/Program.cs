using System;
using FileForward;

namespace FileForwardConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from FileForward!");
            SendFromFile();
            SendByteArray();
        }

        static void SendFromFile()
        {
            Console.WriteLine("Sending from file...");
            var fileForward = new ForwardEngine("https://jsonplaceholder.typicode.com");
            
            var response = fileForward.PostBytes("/photos", "./Assets/raccoon_image.png");
            var result = response.Result; //I know there's better ways to handle this.

            Console.WriteLine($"Result: {result.StatusCode} - {result.ReasonPhrase}");              
        }
        
        static void SendByteArray()
        {
            Console.WriteLine("Sending byte array...");
            var fileForward = new ForwardEngine("https://jsonplaceholder.typicode.com");

            var fileContent = new byte[200];
            
            var response = fileForward.PostBytes("/photos", fileContent);
            var result = response.Result; //I know there's better ways to handle this.

            Console.WriteLine($"Result: {result.StatusCode} - {result.ReasonPhrase}");            
        }
    }
}