﻿namespace System.Validation.Builder;

public class StringValidator : Validator<string?>, IStringValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public StringValidator(string? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<string?, StringValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(string), Source);
    }

    public IConnector<StringValidator> Connector { get; }

    public IConnector<StringValidator> IsNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsNotNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsEmpty() {
        var validator = _commandFactory.Create(nameof(IsEmpty));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsNotEmpty() {
        var validator = _commandFactory.Create(nameof(IsEmpty));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsEmptyOrWhiteSpace() {
        var validator = _commandFactory.Create(nameof(IsEmptyOrWhiteSpace));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsNotEmptyOrWhiteSpace() {
        var validator = _commandFactory.Create(nameof(IsEmptyOrWhiteSpace));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> LengthIsAtLeast(int length) {
        var validator = _commandFactory.Create(nameof(LengthIsAtLeast), length);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> Contains(string substring) {
        var validator = _commandFactory.Create(nameof(Contains), substring);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsIn(params string[] list) {
        var validator = _commandFactory.Create(nameof(IsIn), list.OfType<object?>().ToArray());
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> LengthIs(int length) {
        var validator = _commandFactory.Create(nameof(LengthIs), length);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> LengthIsAtMost(int length) {
        var validator = _commandFactory.Create(nameof(LengthIsAtMost), length);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsEmail() {
        var validator = _commandFactory.Create(nameof(IsEmail));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<StringValidator> IsPassword(IPasswordPolicy policy) {
        var validator = _commandFactory.Create(nameof(IsPassword), policy);
        ValidateWith(validator);
        return Connector;
    }
}