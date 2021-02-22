using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Changes done here.
            var host = CreateHostBuilder(args).Build();

            // using is used to dispose of the scope variable after utilization
            // host.Services will host any services that we create inside this particular method.
            // But as soon as we finish and start our application, we want this to be disposed off because
            // this is where we are going to store any of our services.
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            // getting the database access as a service.
            // and trying the migration 
            // If we dont have a database and if we start our application then we are going to hit this line of code present
            // inside the try block.
            // and we can run on migration and create a database if we didnt already have it.
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context);  //Adds Data into Database.
            }
            // Now maintaining the exception code block
            catch (Exception ex)
            {
                // Logs are maintained at this point.
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An Error occurred during migration");
            }

            // In order to start our application we need to do host.Run(); 
            // Which is a mandatory step in order to start our application. 
            await host.RunAsync(); 


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
