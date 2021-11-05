using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Core.Enums;
using Spotifalso.Infrastructure.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var users = await userService.GetAllAsync();
                if (!users.Any(x => x.Nickname.ToLowerInvariant() == "admin" && (Roles)Enum.Parse(typeof(Roles), x.Role) == Roles.Admin))
                {
                    var userInput = new UserInput
                    {
                        Nickname = "admin",
                        Role = Roles.Admin.ToString(),
                        Password = "admin"
                    };

                    var identities = new ClaimsIdentity(new Claim[]
                                    {
                                            new Claim(ClaimTypes.Name, userInput.Nickname),
                                            new Claim(ClaimTypes.Role, userInput.Role.ToString()),
                                    },
                                    "JWT",
                                    ClaimTypes.Name,
                                    ClaimTypes.Role);

                    var claim = new ClaimsPrincipal(identities);

                    await userService.InsertAsync(userInput, claim);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
