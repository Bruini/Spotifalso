using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.Services;
using Spotifalso.Infrastructure.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotifalso.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            await ApplyMigrations(webHost.Services);
            await CreateDefaultAdminUser(webHost.Services);
            await webHost.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using SpotifalsoDBContext dbContext = scope.ServiceProvider.GetRequiredService<SpotifalsoDBContext>();
            await dbContext.Database.MigrateAsync();
        }

        private static async Task CreateDefaultAdminUser(IServiceProvider serviceProvider)
        {        
            try
            {
                var scope = serviceProvider.CreateScope();
                var userService =  scope.ServiceProvider.GetRequiredService<IUserService>();
                var users = await userService.GetAllAsync();
                if (!users.Any(x => x.Nickname.ToLowerInvariant() == "admin" && x.Role == Core.Enums.Roles.Admin))
                {
                    var userInput = new UserInput
                    {
                        Nickname = "admin",
                        Role = "Admin",
                        Password = "admin"
                    };
                    await userService.InsertAsync(userInput);
                }
            }
            catch(Exception ex)
            {
                throw;
            }        
        }        
    }
}
