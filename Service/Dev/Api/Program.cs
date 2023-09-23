using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Me.JieChen.Lens.Api.Auth;
using Me.JieChen.Lens.Api.Host;
using Me.JieChen.Lens.Api.Options;
using MeLogging = Me.JieChen.Lens.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;


namespace Me.JieChen.Lens.Api;

public class Program
{
    private const string IDENTITY_CONFIG_SECTION = "Identity";
    private const string EXTERNAL_AUTHORITY_ISSUERS = "ValidIssuers";
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        var config = builder.Configuration;

        builder.Host.ConfigureAppConfiguration((context, config) =>
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug().AddConsole());
            var logger = new MeLogging.Logger<Program>(loggerFactory.CreateLogger<Program>());
            var env = context.HostingEnvironment;
            logger.LogInformation($"Working in {env.EnvironmentName} now.");
        });

        BuildAppOptions(config, out AppOptions appOptions);

        setupService(services, config, appOptions);

        startApp(builder, appOptions);
    }

    private static void BuildAppOptions(ConfigurationManager config, out AppOptions appOptions)
    {
        var options = new AppOptions();
        config.Bind(options);

        if (options.Environment.LoadKeyVault)
        {
            config.AddAzureKeyVault(new SecretClient(new Uri($"https://{options.Environment.KeyVault}.vault.azure.net"), new DefaultAzureCredential()), new AzureKeyVaultConfigurationOptions
            {
                Manager = new KeyVaultSecretManager(),
                ReloadInterval = TimeSpan.FromMinutes(15),
            });
        }

        config.Bind(options);
        appOptions = options;
    }

    private static void setupAuthN(IServiceCollection services, ConfigurationManager config)
    {
        services.AddMicrosoftIdentityWebApiAuthentication(config, IDENTITY_CONFIG_SECTION);
        services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters.ValidIssuers = config.GetSection(EXTERNAL_AUTHORITY_ISSUERS).Get<string[]>();
        });
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        services.AddSingleton<IClaimsTransformation, ClientClaimsTransformation>();
    }

    private static void setupService(IServiceCollection services, ConfigurationManager config, AppOptions appOptions)
    {
        services.AddCustomServices(appOptions);
        setupAuthN(services, config);
        services.AddControllers();
    }

    private static void startApp(WebApplicationBuilder builder, AppOptions appOptions)
    {
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        if (appOptions.Environment.Debug)
        {
            app.MapControllers().WithMetadata(new AllowAnonymousAttribute());
        }
        else
        {
            app.MapControllers();
        }

        app.UseCustomMiddlewares(appOptions);

        app.Run();
    }
}