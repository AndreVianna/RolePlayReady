﻿namespace RolePlayReady.Handlers.Sphere;

public record Sphere : Persisted {
    public ICollection<Base> Components { get; init; } = new List<Base>();
    public ICollection<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override ValidationResult Validate() {
        var result = base.Validate();
        result += Components!.ForEach(item => item.IsNotNull().And.IsValid()).Result;
        result += AttributeDefinitions!.ForEach(item => item.IsNotNull().And.IsValid()).Result;
        return result;
    }
}