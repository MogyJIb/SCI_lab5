using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using SCI_lab5.Utils;

namespace SCI_lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DbInitializationUtil.Init();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
