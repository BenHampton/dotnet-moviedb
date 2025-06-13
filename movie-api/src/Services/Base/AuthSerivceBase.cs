// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using movie_api.Authorization;
using movie_api.Data.Dtos;

namespace movie_api.Services.Base;

public abstract class AuthServiceBase
{
    private readonly ILogger<AuthService> _logger;
    private readonly IConfiguration _configuration;

    [Obsolete("For testing only")]
    public AuthServiceBase() { }

    public AuthServiceBase(ILogger<AuthService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

// #if !DISABLE_TOKEN_GENERATION
    public virtual string? GenerateJwt(string role, string username)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, username)
        };

        JwtSecurityToken? token = null;

        var bearer = _configuration.GetSection("Authentication:Schemes:Bearer");
        if (bearer is not null)
        {
            var signingKeys = bearer.GetSection("SigningKeys");
            var firstKey = signingKeys
                .GetChildren()
                .First();

            var uniqueKey = firstKey
                .GetValue<string>("Value");
            var issuer = bearer["ValidIssuer"];
            var audiences = bearer.GetSection("ValidAudiences")
              .Get<string[]>();
            var key = new SymmetricSecurityKey(Convert.FromBase64String(uniqueKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(creds);
            var jwtBody = new JwtPayload(claims)
            {
                { JwtRegisteredClaimNames.Aud, audiences },
                { JwtRegisteredClaimNames.Iss, issuer },
                { JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, DateTimeOffset.Now.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, DateTimeOffset.Now.AddMonths(3).ToUnixTimeSeconds() }
            };

            token = new JwtSecurityToken(jwtHeader, jwtBody);
        }

        if (token is null)
        {
            _logger.LogError("Something went catastrophically wrong and a jwt could not be generated");
            return null;
        }

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
// #endif

    public virtual PermissionsDto? GetUserPermissions(ClaimsIdentity? identity)
    {
        if (identity is null)
        {
            return null;
        }

        var role = identity.FindFirst(ClaimTypes.Role);

        if (role is null)
        {
            _logger.LogWarning("User was not in a role");
            return null;
        }

        var userRole = RoleUtility.GetRole(role.Value);

        return new PermissionsDto()
        {
            Role = role.Value,
            Permissions = userRole.Permissions.Select(p => p.ToString()).ToList(),
        };
    }
}
