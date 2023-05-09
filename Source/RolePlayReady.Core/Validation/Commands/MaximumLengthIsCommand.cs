﻿namespace System.Validation.Commands;

public sealed class LengthIsAtMostCommand
    : ValidationCommand {
    public LengthIsAtMostCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Length <= length;
        ValidationErrorMessage = MustHaveAMaximumLengthOf;
        GetErrorMessageArguments = s => new object?[] { length, ((string)s!).Length };
    }
}