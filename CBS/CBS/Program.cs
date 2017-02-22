namespace CBS
{
    using System;

    using Microsoft.Owin.Hosting;

    internal static class Program
    {
        public static void Main()
        {
            const string Port = "8080";
            var baseAddress = $"http://localhost:{Port}/";
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine($"Server started...on port: {Port}");
                Console.ReadLine();
            }
        }
    }
}
