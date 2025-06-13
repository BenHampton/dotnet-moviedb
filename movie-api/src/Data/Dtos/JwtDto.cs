// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace movie_api.Data.Dtos;

public class JwtDto
{
    public string Role { get; set; } = "Scheduler";

    public string Username { get; set; } = "Jellybeans";
}
