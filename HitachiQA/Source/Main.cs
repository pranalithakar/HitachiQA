using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HitachiQA.Source
{

    public class Main
    {
        public static readonly IConfiguration Configuration = BuildConfig();

        private static IConfiguration BuildConfig()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(BasePath)
                           .AddJsonFile("appsettings.json")
                           .AddEnvironmentVariables()
                           .AddUserSecrets(ExecutingAssembly);

            var config = builder.Build();


            var appConfigUri = config.GetChildren().FirstOrDefault(it => it.Key == "AppConfig")?.Value;
            var keyVaultUri = config.GetChildren().FirstOrDefault(it => it.Key == "KeyVault")?.Value;

            if (IsValid(appConfigUri))
            {
                builder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(new Uri(config["AppConfig"]), new DefaultAzureCredential());
                });
            }

            if(IsValid(keyVaultUri))
            {
                builder.AddAzureKeyVault(new Uri(config["Keyvault"]),
                new DefaultAzureCredential()
                );
            }

            config = builder.Build();      

            return config;

        }
        private static Assembly ExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        private static string BasePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static bool IsValid(string? uri) => uri != null && uri.Length > 0 && uri.ToUpper() != "TBD";
    }


}
