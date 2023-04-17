﻿namespace System.Results.Abstractions;

public interface IResult {
    bool IsSuccessful { get; }
    bool HasErrors { get; }
    ICollection<ValidationError> Errors { get; }
}

public interface IResult<out TValue> : IResult {
    bool HasValue { get; }
    TValue Value { get; }
}