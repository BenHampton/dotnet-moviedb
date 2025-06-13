// using movie_api.Authorization.Google;
// using movie_api.Authorization.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using movie_api.Authorization.Certificates;
using movie_api.Authorization.Handlers;

namespace movie_api.Extensions;

public static class AuthorizationPolicyExtensions
{
    public static Certificate? Certificate { get; set; }
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services, Certificate certificate)
    {
        Certificate = certificate;
        var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == HostingEnvironment.DOCKER ||
                     Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == HostingEnvironment.DEVELOPMENT;

        var authBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        authBuilder.AddJwtBearer();
        // if (!isDev)
        // {
        //     authBuilder.AddJwtBearer(jwtOptions =>
        //         {
        //             if (!isDev)
        //             {
        //                 jwtOptions.TokenValidationParameters = new TokenValidationParameters
        //                 {
        //                     ValidateIssuer = true,
        //                     ValidateAudience = true,
        //                     ValidateLifetime = true,
        //                     ValidateIssuerSigningKey = true,
        //                     ValidIssuer = "https://cloud.google.com/iap",
        //                     // ValidAudience = builder.Configuration["Jwt:Audience"],
        //                     IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
        //                     {
        //                         return GoogleCertificate.keys
        //                         .Where(k => k.kid == kid)
        //                         .Select(k => new X509SecurityKey(k.Cert));
        //                     }
        //                 };
        //             }
        //         });
        // }
        // else
        // {
        //     authBuilder.AddJwtBearer();
        // }

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        return services;
    }
}
