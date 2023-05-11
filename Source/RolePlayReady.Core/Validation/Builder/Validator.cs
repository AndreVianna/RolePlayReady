﻿using static System.Validation.Builder.ValidatorMode;

namespace System.Validation.Builder;

public abstract class Validator : IValidator {
    protected Validator(string source, ValidatorMode mode = And) {
        Mode = mode;
        Source = source;
        Result = ValidationResult.Success();
    }

    public ValidatorMode Mode { get; private set; }
    public string Source { get; }
    public ValidationResult Result { get; private set; }

    public Validator SetMode(ValidatorMode mode) {
        Mode = mode;
        return this;
    }

    public void Negate() => Mode ^= ValidatorMode.Not;

    public void ClearErrors() => Result = ValidationResult.Success();

    public void AddError(ValidationError error) => Result += error;

    public void AddErrors(IEnumerable<ValidationError> errors) => Result += errors.ToArray();
}

public abstract class Validator<TSubject> : Validator {
    protected Validator(TSubject? subject, string source, ValidatorMode mode = And)
        : base(source, mode) {
        Subject = subject;
    }

    public TSubject? Subject { get; }

    protected void ValidateWith(IValidationCommand validator) {
        var rightResult = Mode.Has(Not)
            ? validator.Negate(Subject)
            : validator.Validate(Subject);
        if (Mode.Has(Or) && (rightResult.IsSuccess || Result.IsSuccess)) {
            ClearErrors();
            return;
        }

        AddErrors(rightResult.Errors);
    }
}