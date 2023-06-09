﻿using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public abstract record Attribute<TValue> : IAttribute {
    public required AttributeDefinition Definition { get; init; }

    object? IAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public ValidationResult Validate(IDictionary<string, object?>? context = null) {
        var result = Success();
        result += Definition.DataType.Is().And().IsEqualTo<TValue>().Result;
        result += Definition.Constraints.Aggregate(Success(), (r, c)
            => r + c.Create<TValue>(Definition.Name).Validate(Value));
        return result;
    }
}