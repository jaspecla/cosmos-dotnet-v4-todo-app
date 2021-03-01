namespace todo
{
  using Azure.Extensions.AspNetCore.Configuration.Secrets;
  using Azure.Identity;
  using Azure.Security.KeyVault.Secrets;
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((context, config) =>
               {
                 var builtConfig = config.Build();
                 var secretClient = new SecretClient(new Uri($"{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                   new DefaultAzureCredential());
                 config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
               })
            .UseStartup<Startup>();
  }
}
