﻿namespace RolePlayReady.DataAccess.Repositories.Users;

public record UserData : IKey {
    public Guid Id { get; init; }
    public required string Email { get; init; }
    public HashedSecret? Password { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    public string? Name { get; init; }
    public DateOnly? Birthday { get; init; }
}