// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using movie_api.Services;
using movie_api.Services.Base;
using Serilog;

namespace movie_api.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddDependencyExtensions(this IServiceCollection services,
        string dbConnectionString)
    {
        // services.AddSerilog();

        //database connection
        // services.AddDbContext<PostgresContext>(options =>
        //     options.UseNpgsql(dbConnectionString));

        //service
        services.AddScoped<AuthServiceBase, AuthService>();


        //mapper
        // services.AddAutoMapper(typeof(AutoMapperProfileExtensions));

        //repository
        // services.AddScoped<IPipelinesRepository, PipelinesRepository>();
        // services.AddScoped<ISchedulesRepository, SchedulesRepository>();
        // services.AddScoped<IEtlRepository, EtlRepository>();
        // services.AddScoped<EventTypesRepositoryBase, EventTypesRepository>();

        // HttpContext
        // services.AddHttpContextAccessor();

        // Utilities
        // services.AddScoped<HttpContextUtilitiesBase, HttpContextUtilities>();
        //web services
        // When we have web service connectivity, we can uncomment this. Note, we'll also have to change the reference in ScheduleWsImporter
        // Since the generated code doesn't have an parent/base class, we can't just swap the implementation, and we'll actually have
        // To change the constructor argument
        // services.AddScoped(s => new DataInterfaceClient(DataInterfaceClient.EndpointConfiguration.DataInterfaceWsHttpBinding));
        // services.AddScoped<MockScheduling>();

        return services;
    }
}
