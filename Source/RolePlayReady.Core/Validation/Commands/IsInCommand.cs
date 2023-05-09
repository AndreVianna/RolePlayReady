﻿namespace System.Validation.Commands;

public sealed class IsInCommand<TItem>
    : ValidationCommand {
    public IsInCommand(TItem?[] list, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = i => list.Contains((TItem)i!);
        ValidationErrorMessage = MustBeIn;
        GetErrorMessageArguments = i => new [] { list, i };
    }
}