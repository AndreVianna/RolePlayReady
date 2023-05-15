﻿namespace RolePlayReady.Handlers.Auth;

public record UserRow : Row {
    public required string Name { get; init; }
    public required string Email { get; init; }
}