﻿namespace RoleplayReady.Domain.Models;

public record ElementType
{
    // Name must be unique.
    public required string Name { get; init; }

    public required string Description { get; init; }
}