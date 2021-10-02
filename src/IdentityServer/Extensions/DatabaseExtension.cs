using System;
using IdentityServer.Infrastructure.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddCustomizeDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            

            // Replace with your server version and type.
            // Use 'MariaDbServerVersion' for MariaDB.
            // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
            // For common usages, see pull request #1233.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            // Replace 'YourDbContext' with the name of your own DbContext derived class.
            services.AddDbContext<ApplicationDbContext>(
                dbContextOptions =>
                {
                    dbContextOptions
                        .UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);
                        if(env.IsDevelopment())
                        {
                            dbContextOptions.EnableSensitiveDataLogging() // <-- These two calls are optional but help
                                .EnableDetailedErrors();
                        }
                    dbContextOptions.UseOpenIddict();
                }
                // <-- with debugging (remove for production).
            );
            
        }
    }
}