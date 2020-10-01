using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace GrpcSample.Service
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5000,
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    });
                });
        }
    }
}