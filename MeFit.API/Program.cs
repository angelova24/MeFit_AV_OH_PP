using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;

namespace MeFitAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
                    //DefaultAzureCredentialOptions options = new();
                    //options.ExcludeEnvironmentCredential = false;
                    //options.ExcludeManagedIdentityCredential = true;
                    //options.ExcludeSharedTokenCacheCredential = true;
                    //options.ExcludeVisualStudioCredential = true;
                    //options.ExcludeVisualStudioCodeCredential = true;
                    //options.ExcludeAzureCliCredential = true;
                    //options.ExcludeAzurePowerShellCredential = true;
                    //options.ExcludeInteractiveBrowserCredential = true;
                    //config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential(options));
                    config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
