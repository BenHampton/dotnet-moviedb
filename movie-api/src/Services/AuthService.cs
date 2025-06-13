// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using movie_api.Services.Base;

namespace movie_api.Services;

public class AuthService : AuthServiceBase
{
    public AuthService(ILogger<AuthService> logger, IConfiguration configuration) : base(logger, configuration)
    {
    }
}
