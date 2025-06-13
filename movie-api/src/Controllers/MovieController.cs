// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movie_api.Authorization.Attributes;
using movie_api.Authorization.Permissions;

namespace movie_api.Controllers;

[ApiController]
[Route("/api/v1/movies/")]
[Authorize]
[NeedsPermission(Permission.SCHEDULE_READ)]
public class MovieController: ControllerBase
{

    [HttpGet]
    [EndpointSummary("Finds all movies.")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [NeedsPermission(Permission.SCHEDULE_READ)]
    public async Task<ActionResult<string>> TestSecured()
    {
        return Ok("Hello World");
    }
}
