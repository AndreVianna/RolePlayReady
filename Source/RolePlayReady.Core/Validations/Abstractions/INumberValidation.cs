﻿namespace System.Validations.Abstractions;

public interface INumberValidation<in TValue>
    : IConnectsToOrFinishes<INumberValidation<TValue>>
    where TValue : IComparable<TValue> {
    IConnectsToOrFinishes<INumberValidation<TValue>> GreaterOrEqualTo(TValue minimum);
    IConnectsToOrFinishes<INumberValidation<TValue>> GreaterThan(TValue minimum);
    IConnectsToOrFinishes<INumberValidation<TValue>> EqualTo(TValue value);
    IConnectsToOrFinishes<INumberValidation<TValue>> LessThan(TValue maximum);
    IConnectsToOrFinishes<INumberValidation<TValue>> LessOrEqualTo(TValue maximum);
}