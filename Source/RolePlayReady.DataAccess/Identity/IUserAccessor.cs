﻿namespace RolePlayReady.DataAccess.Identity;

public interface IUserAccessor {
    string Id { get; }
    string BaseFolder { get; }
    string Email { get; }
}