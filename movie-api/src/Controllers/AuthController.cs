// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Security.Claims;
using movie_api.Data.Dtos;
// using movie_api.Services.Base;
// using movie_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movie_api.Services;
using movie_api.Services.Base;

namespace movie_api.Controllers;

[ApiController]
[Route("/api/v1/auth")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly AuthServiceBase _authService;
    private readonly bool _isDev;

    public AuthController(ILogger<AuthController> logger, AuthServiceBase authService)
    {
        _logger = logger;
        _authService = authService;
        _isDev = true;
        // Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == HostingEnvironment.DOCKER ||
        //          Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == HostingEnvironment.DEVELOPMENT;
    }

    [HttpGet]
    [EndpointSummary("Get user permissions")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<PermissionsDto> GetCurrentUserPermissions()
    {
        var permissionsDto = _authService.GetUserPermissions(HttpContext.User.Identity as ClaimsIdentity);

        if (permissionsDto is null)
        {
            _logger.LogInformation("No permissions found for user");
            return Unauthorized();
        }

        return permissionsDto;
    }

#if !DISABLE_TOKEN_GENERATION
    [HttpPost]
    [EndpointSummary("Generate a jwt")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public ActionResult<string?> CreateJwt([FromBody] JwtDto jwtDto)
    {
        if (!_isDev)
        {
            return NotFound();
        }
        return _authService.GenerateJwt(jwtDto.Role, jwtDto.Username);
    }
#endif
}
