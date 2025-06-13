using System.Reflection;
using Google.Apis.Auth.OAuth2;
// using Google.Apis.Auth.OAuth2;
using movie_api;
// using movie_api.Authorization.Google;
using movie_api.Data;
using movie_api.Extensions;
// using movie_api.Services;
// using movie_api.Services.Interfaces;
// using movie_api.Utilities;
// using Google.Apis.Auth.OAuth2;
// using HealthChecks.ApplicationStatus.DependencyInjection;
// using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
// using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using movie_api.Authorization.Certificates;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry()
    .CreateLogger();

try
{
    string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var builder = WebApplication.CreateBuilder(args);


    builder.Configuration.AddUserSecrets("606618ab-ad6e-4b2a-ae7f-9f497d4a6157");

    var corsPolicy = "AllowAll";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: corsPolicy,
            configurePolicy: policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

    var connectionString = "TODO"; //builder.Configuration.GetConnectionString("postgres") ??
    //                        throw new InvalidOperationException("Connection string 'postgres' not found.");

    Certificate? certificate;
    Log.Logger.Information("HostingEnvironment: " + environmentName);
    using (var httpClient = new HttpClient())
    {
        certificate = await httpClient.GetFromJsonAsync<Certificate>(GoogleAuthConsts.IapKeySetUrl);
    }
    // if (environmentName == HostingEnvironment.DOCKER)
    // {
    //     string jsonString = await File.ReadAllTextAsync("Data/GooglePublicKey/public_key-jwk.json");
    //     certificate = JsonSerializer.Deserialize<Certificate>(jsonString)!;
    // }
    // else
    // {
    //     using (var httpClient = new HttpClient())
    //     {
    //         certificate = await httpClient.GetFromJsonAsync<Certificate>(GoogleAuthConsts.IapKeySetUrl);
    //     }
    // }

    if (certificate is null)
    {
        Log.Fatal("Could not get certificate for validation. Exiting");
        return;
    }

    // Add services to the container.
    builder.Services.AddDependencyExtensions(connectionString);
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
        });
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(swagger =>
    {
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
        });

        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });
    builder.Services.AddProblemDetails();
    builder.Services.AddAuthorizationPolicies(certificate);


    var app = builder.Build();

// if (environmentName is HostingEnvironment.DEVELOPMENT or HostingEnvironment.DOCKER)
// {
//     builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
// }

    if (environmentName is HostingEnvironment.DEVELOPMENT or HostingEnvironment.DOCKER)
    {
        using var temporaryScope = app.Services.CreateScope();
        // var context = temporaryScope.ServiceProvider.GetRequiredService<PostgresContext>();

        var isDataRefresh = false;
        bool.TryParse(app.Configuration.GetValue<string>("DATA_REFRESH"), out isDataRefresh);
        // if (isDataRefresh)
        // {
        //     Log.Logger.Information("Running full data refresh");
        //     await context.Database.EnsureDeletedAsync();
        //     await context.Database.MigrateAsync();
        //
        //     var etlService = temporaryScope.ServiceProvider.GetRequiredService<IEtlService>();
        //
        //     await etlService.ImportLineConfigurationsAsync();
        // }
        //
        // if (!isDataRefresh && (await context.Database.GetPendingMigrationsAsync()).Any())
        // {
        //     if (environmentName == HostingEnvironment.DOCKER)
        //     {
        //         Log.Information("Applying Migrations (if any are pending)");
        //         await context.Database.MigrateAsync();
        //     }
        //     else
        //     {
        //         Log.Warning(
        //             "Your data model is out of date. Please consider running 'make update-database' at your earliest convenience");
        //     }
        // }

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();
    app.UseCors(corsPolicy);
    app.UseAuthentication();
    app.UseAuthorization();

    // app.UseResponseCompression();

    app.MapControllers();
    await app.RunAsync();
}
catch (Exception ex)
{
    // Log.Logger.Error(ex.ToString());
    // This is in place to prevent EF migrations from spitting out errors
    // EF migrations don't enter through the main assembly
    // We can check the executing assembly with reflrection and only log the exception
    // if it's running through the entry assembly
    if (Assembly.GetEntryAssembly() == Assembly.GetExecutingAssembly())
    {
        Log.Fatal(ex, "Application terminated unexpectedly");
    }
}
finally
{
    await Log.CloseAndFlushAsync();
}
