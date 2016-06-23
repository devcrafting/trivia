using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Trivia.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:8090/api";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Server started on {baseAddress}.");
                Console.WriteLine("Press Enter to stop it...");
                Console.ReadLine();
            }
        }
    }
}
