﻿namespace System.Validators.Collection;

public sealed class MaximumCountIs<TItem> : CollectionValidator<TItem> {
    private readonly int _count;

    public MaximumCountIs(string source, int count)
        : base(source) {
        _count = count;
    }

    protected override ValidationResult ValidateValue(CollectionValidations<TItem> validation)
        => validation.MaximumCountIs(_count).Result;
}