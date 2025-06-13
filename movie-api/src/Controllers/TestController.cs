// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace movie_api.Controllers;

[ApiController]
[Route("/api/v1/test")]
[AllowAnonymous]
public class TestController: ControllerBase
{

    [HttpGet]
    [EndpointSummary("Test")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> TestUnsecured()
    {
        return Ok("TEST Hello World");
    }

}
