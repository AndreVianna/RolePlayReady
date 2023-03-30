﻿namespace RoleplayReady.Domain.Models;

public record ElementType
{
    // RuleSet and Name must be unique.
    public required RuleSet RuleSet { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}