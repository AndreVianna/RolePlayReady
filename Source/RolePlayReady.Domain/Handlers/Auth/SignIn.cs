﻿namespace RolePlayReady.Handlers.Auth;

public record SignIn : IValidatable {
    public required string Email { get; init; }
    public required string Password { get; init; }

    public ValidationResult Validate(IDictionary<string, object?>? context = null) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
                       .And().IsNotEmptyOrWhiteSpace()
                       .And().IsEmail().Result;
        result += Password.IsRequired()
                        .And().IsNotEmptyOrWhiteSpace()
                        .And().LengthIsAtMost(Validation.Password.MaximumLength).Result;
        return result;
    }
}