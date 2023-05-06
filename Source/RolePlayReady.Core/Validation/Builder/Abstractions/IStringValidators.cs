﻿namespace System.Validation.Builder.Abstractions;

public interface IStringValidators : IValidators {
    IConnectors<string?, StringValidators> IsNotEmptyOrWhiteSpace();
    IConnectors<string?, StringValidators> MinimumLengthIs(int length);
    IConnectors<string?, StringValidators> MaximumLengthIs(int length);
    IConnectors<string?, StringValidators> LengthIs(int length);
    IConnectors<string?, StringValidators> IsIn(params string[] list);
    IConnectors<string?, StringValidators> IsEmail();
    IConnectors<string?, StringValidators> IsPassword(IPasswordPolicy policy);
}