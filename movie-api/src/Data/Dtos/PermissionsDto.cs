// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace movie_api.Data.Dtos;

public class PermissionsDto
{
    public required string Role { get; set; }

    public required List<string> Permissions { get; set; }
}
