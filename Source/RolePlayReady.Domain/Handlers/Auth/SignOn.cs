﻿namespace RolePlayReady.Handlers.Auth;

public record SignOn : IValidatable {
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }

    public ValidationResult Validate(IDictionary<string, object?>? context = null) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsEmail().Result;
        result += Password.IsRequired().Result;
        return result;
    }
}