using System;
using System.Threading.Tasks;
using Navo.Client;

namespace WarehouseDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Navo Warehouse Client Demo");
            var apiUrl = Environment.GetEnvironmentVariable("NAVO_API");
            var clientId = Guid.Parse(Environment.GetEnvironmentVariable("NAVO_CLIENT"));
            var apiKey = Environment.GetEnvironmentVariable("NAVO_API_KEY");
            Console.WriteLine($"Connecting to { apiUrl }...");
            var client = new NavoClient(apiUrl);
            await client.AuthenticateAsync(clientId, apiKey);
            Console.WriteLine("Navo Client authorized.");
            var jobs = await client.Jobs.GetAsync();
            Console.WriteLine($"{ jobs.Count } jobs found.");
            if (jobs.Count == 0)
            {
                Console.WriteLine("No jobs found, skipping demonstration of getting job details.");
                return;
            }
            var job = await client.Jobs.GetAsync(jobs[0].Id);
            Console.WriteLine($"Job { job.Name } requires { job.TotalParts } parts.");
            Console.WriteLine("Warehouse dotnet client demo successfully ran.");
        }
    }
}
