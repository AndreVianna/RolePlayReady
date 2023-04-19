﻿namespace RolePlayReady.Models.Attributes;

public sealed record AttributeConstraint : IAttributeConstraint {

    public AttributeConstraint(string validatorName, params object?[] arguments) {
        ValidatorName = validatorName;
        Arguments = arguments;
    }
    public string ValidatorName { get; }
    public ICollection<object?> Arguments { get; }
}