﻿using System.Validation.Abstractions;

namespace System.Validation;

public class IntegerValidators
    : Validators<int?, IntegerValidators>
    , IIntegerValidators {

    public static IntegerValidators CreateAsOptional(int? subject, string source)
        => new(subject, source);
    public static IntegerValidators CreateAsRequired(int? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private IntegerValidators(int? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<int?, IntegerValidators>(Subject, this);
    }

    public IValidatorsConnector<int?, IntegerValidators> MinimumIs(int threshold) {
        CommandFactory.Create(nameof(MinimumIs), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<int?, IntegerValidators> IsGreaterThan(int threshold) {
        CommandFactory.Create(nameof(IsGreaterThan), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<int?, IntegerValidators> IsEqualTo(int threshold) {
        CommandFactory.Create(nameof(IsEqualTo), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<int?, IntegerValidators> IsLessThan(int threshold) {
        CommandFactory.Create(nameof(IsLessThan), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<int?, IntegerValidators> MaximumIs(int threshold) {
        CommandFactory.Create(nameof(MaximumIs), threshold).Validate();
        return Connector;
    }
}