﻿namespace System.Results;

public record Result<TValue> : ResultBase, IResult<TValue> {
    public Result(TValue input) : this(Ensure.IsNotNull(input), Array.Empty<ValidationError>()) {
    }

    private Result(object? input, IEnumerable<ValidationError> errors) {
        Value = input switch {
            TValue value => value,
            null => throw new InvalidCastException(string.Format(IsNotOfType, nameof(input), typeof(TValue).Name, "null")),
            _ => throw new InvalidCastException(string.Format(IsNotOfType, nameof(input), typeof(TValue).Name, input.GetType().Name))
        };

        foreach (var error in errors) Errors.Add(error);
    }

    public bool HasValue => true;

    [NotNull]
    public TValue Value { get; }

    public static implicit operator Result<TValue>(NullableResult<object> value) => new(value.Value, value.Errors);
    public static implicit operator Result<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue(Result<TValue> input) => input.Value;

    public static Result<TValue> operator +(Result<TValue> left, ValidationResult right) => left with { Errors = left.Errors.Union(right.Errors).ToList() };
    public static Result<TValue> operator +(ValidationResult left, Result<TValue> right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };
    public static bool operator ==(Result<TValue> left, ValidationResult right) => ReferenceEquals(right, ValidationResult.Success) && left.IsSuccessful;
    public static bool operator !=(Result<TValue> left, ValidationResult right) => !ReferenceEquals(right, ValidationResult.Success) || !left.IsSuccessful;

    public virtual bool Equals(Result<TValue>? other)
        => other is not null
           && Value.Equals(other.Value)
           && Errors.SequenceEqual(other.Errors);
}